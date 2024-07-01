using System.Security.Claims;
using AutoMapper;
using Medhub_Backend.Application.Abstractions.Service;
using Medhub_Backend.Application.Dtos.TestResult;
using Medhub_Backend.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedHub_Backend.WebApi.Controller;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class TestResultController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ITestRequestService _testRequestService;
    private readonly ITestResultService _testResultService;
    private readonly IUserService _userService;

    public TestResultController(
        ITestResultService testResultService,
        IUserService userService,
        ITestRequestService testRequestService,
        IMapper mapper)
    {
        _testResultService = testResultService;
        _userService = userService;
        _testRequestService = testRequestService;
        _mapper = mapper;
    }

    [Authorize(Roles = "Admin, Doctor")]
    [HttpPost]
    [ProducesResponseType(200, Type = typeof(TestResultDto))]
    public async Task<IActionResult> AddTestResult([FromForm] CreateTestResultRequest testResultRequest, IFormFile formFile)
    {
        if (formFile == null) return BadRequest(ModelState);

        var testRequest = await _testRequestService.GetByIdAsync(testResultRequest.TestRequestId);
        if (testRequest == null) return NotFound($"Test request with id {testResultRequest.TestRequestId} not found");

        var validTestTypeIds = testRequest.TestTypes.Select(tt => tt.Id).ToList();
        var invalidTestTypeIds = testResultRequest.TestTypesIds.Except(validTestTypeIds).ToList();
        if (invalidTestTypeIds.Any()) return BadRequest("Invalid test type IDs for this test request");

        var testResult = _mapper.Map<TestResult>(testResultRequest);
        var createdTestResult = await _testResultService.CreateTestResultWithFile(testResult, testResultRequest.TestTypesIds, testRequest, formFile);
        var testResultDto = _mapper.Map<TestResultDto>(createdTestResult);

        return Ok(testResultDto);
    }

    [Authorize]
    [HttpGet("{testResultId}")]
    [ProducesResponseType(200, Type = typeof(FileResult))]
    public async Task<IActionResult> DownloadPdf([FromRoute] int testResultId)
    {
        var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(username))
            return Unauthorized("Invalid user");

        var user = await _userService.GetByUsernameAsync(username);
        if (user == null)
            return Unauthorized("Invalid user");

        var testResult = await _testResultService.GetByIdAsync(testResultId);
        if (testResult == null) return NotFound();

        if ((user.Role.Name == "Patient" && testResult.TestRequest.PatientId != user.Id) ||
            (user.Role.Name == "Doctor" && testResult.TestRequest.DoctorId != user.Id) ||
            (user.Role.Name != "Patient" && user.Role.Name != "Doctor"))
            return Unauthorized("User not authorized to access this result");

        var result = await _testResultService.DownloadTestResultPdf(testResultId);
        if (result == null) return NotFound();

        var (fileBytes, contentType, fileName) = result.Value;
        return File(fileBytes, contentType, fileName);
    }

    [HttpGet("{testResultId}/info")]
    [ProducesResponseType(200, Type = typeof(TestResultDto))]
    [ProducesResponseType(4004)]
    public async Task<IActionResult> GetTestResult([FromRoute] int testResultId)
    {
        var testResult = await _testResultService.GetByIdAsync(testResultId);
        if (testResult is null) return NotFound($"Test Result with id {testResultId} not found");

        var testResultDto = _mapper.Map<TestResultDto>(testResult);
        return Ok(testResultDto);
    }

    [Authorize(Roles = "Admin, Doctor")]
    [HttpDelete("{testResultId}")]
    public async Task<IActionResult> DeleteTestResult([FromRoute] int testResultId)
    {
        await _testResultService.DeleteByIdAsync(testResultId);
        return Ok();
    }

    [Authorize(Roles = "Admin, Doctor")]
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(List<TestResultDto>))]
    public IActionResult GetAllTestResults()
    {
        var testResults = _testResultService.GetAllAsync();
        var testResultsDto = _mapper.Map<List<TestResultDto>>(testResults);
        return Ok(testResultsDto);
    }
}