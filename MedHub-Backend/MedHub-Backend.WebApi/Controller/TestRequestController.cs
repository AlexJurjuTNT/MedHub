using System.ComponentModel.DataAnnotations;
using AutoMapper;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Medhub_Backend.Business.Dtos.TestRequest;
using Medhub_Backend.Business.Dtos.TestResult;
using Medhub_Backend.Business.Dtos.TestType;
using Medhub_Backend.Business.Service.Interface;
using Medhub_Backend.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;

namespace MedHub_Backend.WebApi.Controller;

[ApiController]
[Route("api/v1/[controller]")]
public class TestRequestController : ControllerBase
{
    private readonly ILaboratoryService _laboratoryService;
    private readonly IMapper _mapper;
    private readonly ITestRequestService _testRequestService;
    private readonly ITestTypeService _testTypeService;
    private readonly IUserService _userService;
    private readonly IDateTimeProvider _dateTimeProvider;

    public TestRequestController(
        ITestRequestService testRequestService,
        ITestTypeService testTypeService,
        IUserService userService,
        ILaboratoryService laboratoryService,
        IMapper mapper, IDateTimeProvider dateTimeProvider)
    {
        _testRequestService = testRequestService;
        _testTypeService = testTypeService;
        _userService = userService;
        _laboratoryService = laboratoryService;
        _mapper = mapper;
        _dateTimeProvider = dateTimeProvider;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(List<TestRequestDto>))]
    public async Task<IActionResult> GetAllTestRequests()
    {
        var testRequests = await _testRequestService.GetAllTestRequestsAsync();
        var testRequestsDtos = _mapper.Map<List<TestRequestDto>>(testRequests);
        return Ok(testRequestsDtos);
    }

    [HttpGet("{testRequestId}")]
    [ProducesResponseType(200, Type = typeof(TestRequestDto))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetTestRequestById(int testRequestId)
    {
        var testRequest = await _testRequestService.GetTestRequestByIdAsync(testRequestId);
        if (testRequest == null) return NotFound($"Test request with id {testRequestId} not found");

        var testRequestDto = _mapper.Map<TestRequestDto>(testRequest);
        return Ok(testRequestDto);
    }

    [HttpPost]
    [ProducesResponseType(201, Type = typeof(TestRequestDto))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> CreateTestRequest([FromBody] AddTestRequestDto testRequestDto)
    {
        var (patient, doctor, laboratory, testTypes) = await ValidateAndGetEntities(testRequestDto);

        var testRequest = new TestRequest
        {
            PatientId = patient.Id,
            DoctorId = doctor.Id,
            LaboratoryId = laboratory.Id,
            TestTypes = testTypes,
            RequestDate = _dateTimeProvider.UtcNow,
            ClinicId = doctor.ClinicId
        };

        var createdTestRequest = await _testRequestService.CreateNewTestRequestAsync(testRequest, doctor.Clinic);
        return CreatedAtAction(nameof(GetTestRequestById), new { testRequestId = createdTestRequest.Id }, _mapper.Map<TestRequestDto>(createdTestRequest));
    }

    [HttpPut("{testRequestId}")]
    [ProducesResponseType(200, Type = typeof(TestRequestDto))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateTestRequest(int testRequestId, [FromBody] UpdateTestRequestDto testRequestDto)
    {
        if (testRequestId != testRequestDto.Id) return BadRequest("Id mismatch");

        var patient = await ValidateUser(testRequestDto.PatientId, "Patient");
        var doctor = await ValidateUser(testRequestDto.DoctorId, "Doctor");

        var existingTestRequest = await _testRequestService.GetTestRequestByIdAsync(testRequestId) ??
                                  throw new NotFoundException($"Test request with id {testRequestId} not found");

        var testRequest = _mapper.Map<TestRequest>(testRequestDto);
        var updatedTestRequest = await _testRequestService.UpdateTestRequestAsync(testRequest);
        var requestDto = _mapper.Map<TestRequestDto>(updatedTestRequest);
        return Ok(requestDto);
    }

    [HttpDelete("{testRequestId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteTestRequest(int testRequestId)
    {
        var result = await _testRequestService.DeleteTestRequestAsync(testRequestId);
        return result ? NoContent() : NotFound($"Test request with id {testRequestId} not found");
    }

    [HttpGet("{testRequestId}/results")]
    [ProducesResponseType(200, Type = typeof(List<TestResultDto>))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetAllResultsOfRequest(int testRequestId)
    {
        var testRequest = await _testRequestService.GetTestRequestByIdAsync(testRequestId);
        if (testRequest == null) return NotFound($"Test request with id {testRequestId} not found");

        return Ok(_mapper.Map<List<TestResultDto>>(testRequest.TestResults));
    }

    [HttpGet("{testRequestId}/remaining")]
    [ProducesResponseType(200, Type = typeof(List<TestTypeDto>))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetRemainingTestTypes(int testRequestId)
    {
        var remainingTestTypes = await _testRequestService.GetRemainingTestTypesAsync(testRequestId);
        var testTypeDtos = _mapper.Map<List<TestTypeDto>>(remainingTestTypes);
        return Ok(testTypeDtos);
    }

    // [HttpGet("user/{userId}")]
    // [ProducesResponseType(200, Type = typeof(List<TestRequestDto>))]
    // [ProducesResponseType(400)]
    // [ProducesResponseType(404)]
    // public async Task<IActionResult> GetAllTestRequestsOfUser(int userId)
    // {
    //     var patient = await ValidateUser(userId, "Patient");
    //     var testRequests = await _testRequestService.GetAllTestRequestsOfUserAsync(patient.Id);
    //     var testRequestDtos = _mapper.Map<List<TestRequestDto>>(testRequests);
    //     return Ok(testRequestDtos);
    // }

    [HttpGet("user/clinic/tests")]
    [ProducesResponseType(200, Type = typeof(LoadResult))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetAllTestRequestsOfUserInClinic([FromQuery] int userId, [FromQuery] DataSourceLoadOptions loadOptions)
    {
        var patient = await ValidateUser(userId, "Patient");
        var testRequestsQuery = _testRequestService.GetAllTestRequestsOfUserInClinicAsync(patient.Id, patient.ClinicId);

        var loadedTestRequests = await DataSourceLoader.LoadAsync(testRequestsQuery, loadOptions);

        loadedTestRequests.data = _mapper.Map<List<TestRequestDto>>(loadedTestRequests.data);

        return Ok(loadedTestRequests);
    }

    private async Task<(User patient, User doctor, Laboratory laboratory, List<TestType> testTypes)> ValidateAndGetEntities(AddTestRequestDto testRequestDto)
    {
        var patient = await ValidateUser(testRequestDto.PatientId, "Patient");
        var doctor = await ValidateUser(testRequestDto.DoctorId, "Doctor");

        var laboratory = await _laboratoryService.GetLaboratoryByIdAsync(testRequestDto.LaboratoryId)
                         ?? throw new NotFoundException($"Laboratory with id {testRequestDto.LaboratoryId} not found");

        var testTypes = await _testTypeService.GetTestTypesFromIdList(testRequestDto.TestTypesId);
        if (testTypes.Count != testRequestDto.TestTypesId.Count) throw new ValidationException("One or more test types were not found");

        return (patient, doctor, laboratory, testTypes);
    }

    private async Task<User> ValidateUser(int userId, string expectedRole)
    {
        var user = await _userService.GetUserByIdAsync(userId)
                   ?? throw new NotFoundException($"User with id {userId} not found");

        if (user.Role.Name != expectedRole)
            throw new ValidationException($"User with id {userId} is not a {expectedRole}");

        return user;
    }
}