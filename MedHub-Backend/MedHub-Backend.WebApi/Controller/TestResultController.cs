using System.Security.Claims;
using AutoMapper;
using Medhub_Backend.Business.Dtos.TestResult;
using Medhub_Backend.Business.Service.Interface;
using Medhub_Backend.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedHub_Backend.WebApi.Controller;

[ApiController]
[Route("api/v1/[controller]")]
public class TestResultController : ControllerBase
{
    private readonly ITestResultService _testResultService;
    private readonly IUserService _userService;
    private readonly ITestRequestService _testRequestService;
    private readonly IMapper _mapper;

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

    [Authorize]
    [HttpPost]
    [ProducesResponseType(200, Type = typeof(TestResultDto))]
    public async Task<IActionResult> AddTestResult([FromForm] AddTestResultDto testResultDto, IFormFile formFile)
    {
        if (!ModelState.IsValid || formFile == null) return BadRequest(ModelState);

        try
        {
            var testRequest = await _testRequestService.GetTestRequestByIdAsync(testResultDto.TestRequestId);
            if (testRequest == null)
            {
                return NotFound($"Test request with id {testResultDto.TestRequestId} not found");
            }

            if (!ValidateTestTypeIds(testRequest, testResultDto.TestTypesIds))
            {
                return BadRequest("Invalid test type IDs for this test request");
            }

            var testResult = _mapper.Map<TestResult>(testResultDto);
            var createdTestResult = await _testResultService.CreateTestResultWithFile(testResult, testResultDto.TestTypesIds, testRequest, formFile);
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
        var user = await GetCurrentUser();
        if (user == null)
            return Unauthorized("Invalid user");

        var testResult = await _testResultService.GetTestResultByIdAsync(resultId);
        if (testResult == null) return NotFound();

        if (!IsUserAuthorizedForTestResult(user, testResult))
            return Unauthorized("User not authorized to access this result");

        var result = await _testResultService.DownloadTestResultPdf(resultId);
        if (result == null) return NotFound();

        var (fileBytes, contentType, fileName) = result.Value;
        return File(fileBytes, contentType, fileName);
    }

    [HttpDelete("{testResultId}")]
    public async Task<IActionResult> DeleteTestResult([FromRoute] int testResultId)
    {
        var result = await _testResultService.DeleteTestResultAsync(testResultId);
        if (!result) return NotFound($"Test result with id {testResultId} not found");

        return Ok();
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(List<TestResultDto>))]
    public async Task<IActionResult> GetAllTestResults()
    {
        var testResults = await _testResultService.GetAllTestResultsAsync();
        var testResultsDto = _mapper.Map<List<TestResultDto>>(testResults);
        return Ok(testResultsDto);
    }

    private async Task<User?> GetCurrentUser()
    {
        var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(username))
            return null;

        return await _userService.GetUserByUsernameAsync(username);
    }

    private bool IsUserAuthorizedForTestResult(User user, TestResult testResult)
    {
        if (user.Role.Name == "Patient")
            return testResult.TestRequest.PatientId == user.Id;

        if (user.Role.Name == "Doctor")
            return testResult.TestRequest.DoctorId == user.Id;

        return false;
    }

    private bool ValidateTestTypeIds(TestRequest testRequest, List<int> testTypeIds)
    {
        var validTestTypeIds = testRequest.TestTypes.Select(tt => tt.Id).ToList();
        var invalidTestTypeIds = testTypeIds.Except(validTestTypeIds).ToList();
        return !invalidTestTypeIds.Any();
    }
}