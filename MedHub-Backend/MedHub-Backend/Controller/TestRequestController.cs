using AutoMapper;
using MedHub_Backend.Dto.TestRequest;
using MedHub_Backend.Dto.TestResult;
using MedHub_Backend.Dto.TestType;
using MedHub_Backend.Model;
using MedHub_Backend.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MedHub_Backend.Controller;

[ApiController]
[Route("api/v1/[controller]")]
public class TestRequestController(
    ITestRequestService testRequestService,
    ITestTypeService testTypeService,
    IUserService userService,
    IClinicService clinicService,
    IEmailService emailService,
    IMapper mapper
) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(List<TestRequestDto>))]
    public async Task<IActionResult> GetAllTestRequests()
    {
        var testRequests = await testRequestService.GetAllTestRequestsAsync();
        var testRequestsDto = mapper.Map<List<TestRequestDto>>(testRequests);
        return Ok(testRequestsDto);
    }

    [HttpGet("{testRequestId}")]
    [ProducesResponseType(200, Type = typeof(TestRequestDto))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetTestRequestById([FromRoute] int testRequestId)
    {
        var testRequest = await testRequestService.GetTestRequestByIdAsync(testRequestId);
        if (testRequest == null) return NotFound($"Test request with id {testRequestId} not found");

        var testRequestDto = mapper.Map<TestRequestDto>(testRequest);
        testRequestDto.RequestDate = testRequest.RequestDate.ToString("yyyy-mm-dd HH:mm");
        return Ok(testRequestDto);
    }

    [HttpPost]
    [ProducesResponseType(201, Type = typeof(AddTestRequestDto))]
    public async Task<IActionResult> CreateTestRequest([FromBody] AddTestRequestDto testRequestDto)
    {
        var userPatient = await userService.GetUserByIdAsync(testRequestDto.PatientId);
        if (userPatient == null) return NotFound($"Patient with id {testRequestDto.PatientId} not found");
        if (userPatient.Role.Name != "Patient") return BadRequest($"User with id {testRequestDto.PatientId} is not a patient");

        var userDoctor = await userService.GetUserByIdAsync(testRequestDto.DoctorId);
        if (userDoctor == null) return NotFound($"Doctor with id {testRequestDto.DoctorId} not found");
        if (userDoctor.Role.Name != "Doctor") return BadRequest($"User with id {testRequestDto.DoctorId} is not a doctor");

        var clinic = await clinicService.GetClinicByIdAsync(userDoctor.ClinicId);
        if (clinic == null) return NotFound($"Clinic with id {userDoctor.ClinicId} not found");

        try
        {
            var testRequest = mapper.Map<TestRequest>(testRequestDto);
            testRequest.RequestDate = DateTime.Now;
            var testTypes = await testTypeService.GetTestTypesFromIdList(testRequestDto.TestTypesId);
            var createdTestRequest = await testRequestService.CreateNewTestRequestAsync(testRequest, testTypes);

            await emailService.SendCreatedTestRequestEmail(clinic, createdTestRequest);

            return CreatedAtAction(nameof(GetTestRequestById), new { testRequestId = createdTestRequest.Id }, mapper.Map<TestRequestDto>(createdTestRequest));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{testRequestId}")]
    [ProducesResponseType(200, Type = typeof(TestRequestDto))]
    public async Task<IActionResult> UpdateTestRequest([FromRoute] int testRequestId, [FromBody] TestRequestDto testRequestDto)
    {
        if (testRequestId != testRequestDto.Id) return BadRequest();

        var patientUser = await userService.GetUserByIdAsync(testRequestDto.PatientId);
        if (patientUser == null) return NotFound($"Patient with id {testRequestDto.PatientId} not found");
        if (patientUser.Role.Name != "Patient") return BadRequest($"User with id {testRequestDto.PatientId} is not a patient");

        var doctorUser = await userService.GetUserByIdAsync(testRequestDto.DoctorId);
        if (doctorUser == null) return NotFound($"Doctor with id {testRequestDto.DoctorId} not found");
        if (doctorUser.Role.Name != "Doctor") return BadRequest($"User with id {testRequestDto.DoctorId} is not a doctor");

        var existingTestRequest = await testRequestService.GetTestRequestByIdAsync(testRequestId);
        if (existingTestRequest == null) return NotFound($"Patient with id {testRequestId} not found");

        var testRequest = mapper.Map<TestRequest>(testRequestDto);
        var updatedTestRequest = await testRequestService.UpdateTestRequestAsync(testRequest);
        return Ok(mapper.Map<TestRequestDto>(updatedTestRequest));
    }

    [HttpDelete("{testRequestId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteTestRequest([FromRoute] int testRequestId)
    {
        var result = await testRequestService.DeleteTestRequestAsync(testRequestId);
        if (!result) return NotFound($"Test request with id {testRequestId} not found");

        return NoContent();
    }

    [HttpGet("{testRequestId}/results")]
    [ProducesResponseType(200, Type = typeof(List<TestResultDto>))]
    public async Task<IActionResult> GetAllResultsOfRequest([FromRoute] int testRequestId)
    {
        var testRequest = await testRequestService.GetTestRequestByIdAsync(testRequestId);
        if (testRequest == null) return NotFound($"Test request with id {testRequestId} not found");

        var testResultsDto = mapper.Map<List<TestResultDto>>(testRequest.TestResults);
        return Ok(testResultsDto);
    }

    [HttpGet("{testRequestId}/remaining")]
    [ProducesResponseType(200, Type = typeof(List<TestTypeDto>))]
    public async Task<IActionResult> GetRemainingTestTypes([FromRoute] int testRequestId)
    {
        var testRequest = await testRequestService.GetTestRequestByIdAsync(testRequestId);
        if (testRequest == null) return NotFound($"Test request with id {testRequestId} not found");

        var existingTestTypeIds = await testRequestService.GetExistingTestTypeIdsForTestRequestAsync(testRequestId);
        var remainingTestTypes = testRequest.TestTypes
            .Where(tt => !existingTestTypeIds.Contains(tt.Id))
            .Select(tt => mapper.Map<TestTypeDto>(tt))
            .ToList();

        return Ok(remainingTestTypes);
    }
}