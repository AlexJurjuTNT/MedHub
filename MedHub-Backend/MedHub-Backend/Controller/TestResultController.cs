using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using MedHub_Backend.Dto.TestResult;
using MedHub_Backend.Model;
using MedHub_Backend.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedHub_Backend.Controller;

[ApiController]
[Route("api/v1/[controller]")]
public class TestResultController(
    ITestResultService testResultService,
    IFileService fileService,
    ITestRequestService testRequestService,
    IClinicService clinicService,
    IUserService userService,
    IMapper mapper
) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(200, Type = typeof(TestResultDto))]
    public async Task<IActionResult> AddTestResult([FromForm] AddTestResultDto testResultDto, IFormFile formFile)
    {
        var testResult = mapper.Map<TestResult>(testResultDto);
        var testRequest = await testRequestService.GetTestRequestByIdAsync(testResult.TestRequestId);
        if (testRequest == null) return NotFound($"Test request with id {testResult.TestRequestId} not found");

        var result = await testResultService.UploadResult(testResult, testRequest, formFile);
        return Ok(mapper.Map<TestResultDto>(result));
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(List<TestResultDto>))]
    public async Task<IActionResult> GetAllTestResults()
    {
        var testResults = await testResultService.GetAllTestResultsAsync();
        var testResultsDto = mapper.Map<List<TestResultDto>>(testResults);
        return Ok(testResultsDto);
    }

    [Authorize]
    [HttpGet("{resultId}")]
    [ProducesResponseType(200, Type = typeof(FileResult))]
    public async Task<IActionResult> DownloadPdf([FromRoute] int resultId)
    {
        string authHeader = Request.Headers["Authorization"];
        if (authHeader == null || !authHeader.StartsWith("Bearer")) return Unauthorized();

        var tokenString = authHeader.Substring("Bearer ".Length);
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(tokenString);

        var clinicId = token.Claims.FirstOrDefault(c => c.Type == "ClinicId")?.Value;
        var username = token.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

        if (clinicId == null || username == null) return Unauthorized();

        var clinic = await clinicService.GetClinicByIdAsync(int.Parse(clinicId));
        if (clinic == null) return NotFound($"Clinic with id {clinicId} not found");

        var user = await userService.GetUserByUsernameAsync(username);
        if (user == null) return NotFound($"User with username {username} not found");

        var testResult = await testResultService.GetTestResultByIdAsync(resultId);
        if (testResult == null) return NotFound($"Result with id {resultId} not found");


        if (testResult.TestRequest.PatientId != user.Id ||
            testResult.TestRequest.DoctorId != user.Id ||
            testResult.TestRequest.Patient.ClinicId != user.ClinicId)
            return Unauthorized();

        var pdfPath = testResult.FilePath;
        var pdf = await fileService.DownloadFile(pdfPath);
        return File(pdf.Item1, pdf.Item2, pdf.Item3);
    }
}