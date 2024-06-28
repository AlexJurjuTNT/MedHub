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

    [HttpPost]
    [ProducesResponseType(200, Type = typeof(TestResultDto))]
    public async Task<IActionResult> AddTestResult([FromForm] CreateTestResultRequest testResultRequest, IFormFile formFile)
    {
        if (!ModelState.IsValid || formFile == null) return BadRequest(ModelState);

        try
        {
            var testRequest = await _testRequestService.GetByIdAsync(testResultRequest.TestRequestId);
            if (testRequest == null) return NotFound($"Test request with id {testResultRequest.TestRequestId} not found");

            var validTestTypeIds = testRequest.TestTypes.Select(tt => tt.Id).ToList();
            var invalidTestTypeIds = testResultRequest.TestTypesIds.Except(validTestTypeIds).ToList();
            if (invalidTestTypeIds.Any()) return BadRequest("Invalid test type IDs for this test request");

            var testResult = _mapper.Map<TestResult>(testResultRequest);
            var createdTestResult = await _testResultService.CreateTestResultWithFile(testResult, testResultRequest.TestTypesIds, testRequest, formFile);
            return Ok(_mapper.Map<TestResultDto>(createdTestResult));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize]
    [HttpGet("{resultId}")]
    [ProducesResponseType(200, Type = typeof(FileResult))]
    public async Task<IActionResult> DownloadPdf([FromRoute] int resultId)
    {
        var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(username))
            return Unauthorized("Invalid user");

        var user = await _userService.GetByUsernameAsync(username);
        if (user == null)
            return Unauthorized("Invalid user");

        var testResult = await _testResultService.GetByIdAsync(resultId);
        if (testResult == null) return NotFound();

        if ((user.Role.Name == "Patient" && testResult.TestRequest.PatientId != user.Id) ||
            (user.Role.Name == "Doctor" && testResult.TestRequest.DoctorId != user.Id) ||
            (user.Role.Name != "Patient" && user.Role.Name != "Doctor"))
        {
            return Unauthorized("User not authorized to access this result");
        }

        var result = await _testResultService.DownloadTestResultPdf(resultId);
        if (result == null) return NotFound();

        var (fileBytes, contentType, fileName) = result.Value;
        return File(fileBytes, contentType, fileName);
    }

    [HttpDelete("{testResultId}")]
    public async Task<IActionResult> DeleteTestResult([FromRoute] int testResultId)
    {
        var result = await _testResultService.DeleteByIdAsync(testResultId);
        if (!result) return NotFound($"Test result with id {testResultId} not found");

        return Ok();
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(List<TestResultDto>))]
    public IActionResult GetAllTestResults()
    {
        var testResults = _testResultService.GetAllAsync();
        var testResultsDto = _mapper.Map<List<TestResultDto>>(testResults);
        return Ok(testResultsDto);
    }
}