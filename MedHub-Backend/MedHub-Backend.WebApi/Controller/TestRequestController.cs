using AutoMapper;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Medhub_Backend.Application.Abstractions.Service;
using Medhub_Backend.Application.Dtos.TestRequest;
using Medhub_Backend.Application.Dtos.TestResult;
using Medhub_Backend.Application.Dtos.TestType;
using Medhub_Backend.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedHub_Backend.WebApi.Controller;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class TestRequestController : ControllerBase
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ILaboratoryService _laboratoryService;
    private readonly IMapper _mapper;
    private readonly ITestRequestService _testRequestService;
    private readonly ITestTypeService _testTypeService;
    private readonly IUserService _userService;

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
        var testRequests = await _testRequestService.GetAllAsync().ToListAsync();
        var testRequestsDtos = _mapper.Map<List<TestRequestDto>>(testRequests);
        return Ok(testRequestsDtos);
    }

    [HttpGet("{testRequestId}")]
    [ProducesResponseType(200, Type = typeof(TestRequestDto))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetTestRequestById(int testRequestId)
    {
        var testRequest = await _testRequestService.GetByIdAsync(testRequestId);
        if (testRequest == null) return NotFound($"Test request with id {testRequestId} not found");

        var testRequestDto = _mapper.Map<TestRequestDto>(testRequest);
        return Ok(testRequestDto);
    }

    [Authorize(Roles = "Admin, Doctor")]
    [HttpPost]
    [ProducesResponseType(201, Type = typeof(TestRequestDto))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> CreateTestRequest([FromBody] CreateTestRequestRequest testRequestRequest)
    {
        var patient = await _userService.GetByIdAsync(testRequestRequest.PatientId);
        if (patient == null || patient.Role.Name != "Patient")
            return BadRequest($"Invalid or non-existent patient with id {testRequestRequest.PatientId}");

        var doctor = await _userService.GetByIdAsync(testRequestRequest.DoctorId);
        if (doctor == null || doctor.Role.Name != "Doctor")
            return BadRequest($"Invalid or non-existent doctor with id {testRequestRequest.DoctorId}");

        var laboratory = await _laboratoryService.GetByIdAsync(testRequestRequest.LaboratoryId);
        if (laboratory == null)
            return NotFound($"Laboratory with id {testRequestRequest.LaboratoryId} not found");

        var testTypes = await _testTypeService.GetTestTypesFromIdList(testRequestRequest.TestTypesId);
        if (testTypes.Count != testRequestRequest.TestTypesId.Count)
            return BadRequest("One or more test types were not found");

        var testRequest = new TestRequest
        {
            PatientId = patient.Id,
            DoctorId = doctor.Id,
            LaboratoryId = laboratory.Id,
            TestTypes = testTypes,
            RequestDate = _dateTimeProvider.UtcNow,
            ClinicId = doctor.ClinicId
        };

        var createdTestRequest = await _testRequestService.CreateAsync(testRequest, doctor.Clinic);
        return CreatedAtAction(nameof(GetTestRequestById), new { testRequestId = createdTestRequest.Id }, _mapper.Map<TestRequestDto>(createdTestRequest));
    }

    [Authorize(Roles = "Admin, Doctor")]
    [HttpPut("{testRequestId}")]
    [ProducesResponseType(200, Type = typeof(TestRequestDto))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateTestRequest(int testRequestId, [FromBody] UpdateTestRequestDto request)
    {
        if (testRequestId != request.Id) return BadRequest("Id mismatch");

        var patient = await _userService.GetByIdAsync(request.PatientId);
        if (patient == null || patient.Role.Name != "Patient")
            return BadRequest($"Invalid or non-existent patient with id {request.PatientId}");

        var doctor = await _userService.GetByIdAsync(request.DoctorId);
        if (doctor == null || doctor.Role.Name != "Doctor")
            return BadRequest($"Invalid or non-existent doctor with id {request.DoctorId}");

        var existingTestRequest = await _testRequestService.GetByIdAsync(testRequestId);
        if (existingTestRequest == null)
            return NotFound($"Test request with id {testRequestId} not found");

        _mapper.Map(request, existingTestRequest);
        var testTypes = await _testTypeService.GetTestTypesFromIdList(request.TestTypesIds);
        existingTestRequest.TestTypes = testTypes;
        var updatedTestRequest = await _testRequestService.UpdateAsync(existingTestRequest);
        var updatedUserDto = _mapper.Map<TestRequestDto>(updatedTestRequest);
        return Ok(updatedUserDto);
    }

    [Authorize(Roles = "Admin, Doctor")]
    [HttpDelete("{testRequestId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteTestRequest(int testRequestId)
    {
        var result = await _testRequestService.DeleteAsync(testRequestId);
        return result ? NoContent() : NotFound($"Test request with id {testRequestId} not found");
    }

    [HttpGet("{testRequestId}/results")]
    [ProducesResponseType(200, Type = typeof(List<TestResultDto>))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetAllResultsOfRequest(int testRequestId)
    {
        var testRequest = await _testRequestService.GetByIdAsync(testRequestId);
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

    [HttpGet("user/clinic/tests")]
    [ProducesResponseType(200, Type = typeof(LoadResult))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetAllTestRequestsOfUserInClinic([FromQuery] int userId, [FromQuery] DataSourceLoadOptions loadOptions)
    {
        var patient = await _userService.GetByIdAsync(userId);
        if (patient == null || patient.Role.Name != "Patient")
            return BadRequest($"Invalid or non-existent patient with id {userId}");

        var testRequestsQuery = _testRequestService.GetAllTestRequestsOfUserInClinicAsync(patient.Id, patient.ClinicId);
        var loadedTestRequests = await DataSourceLoader.LoadAsync(testRequestsQuery, loadOptions);
        loadedTestRequests.data = _mapper.Map<List<TestRequestDto>>(loadedTestRequests.data);
        return Ok(loadedTestRequests);
    }
}