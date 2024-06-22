using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Medhub_Backend.Business.Dtos.TestResult;
using Medhub_Backend.Business.Service.Interface;
using Medhub_Backend.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedHub_Backend.WebApi.Controller;

[ApiController]
[Route("api/v1/[controller]")]
public class TestResultController(
    ITestResultService testResultService,
    IFileService fileService,
    ITestRequestService testRequestService,
    IClinicService clinicService,
    IUserService userService,
    ITestTypeService testTypeService,
    IMapper mapper
) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(200, Type = typeof(TestResultDto))]
    public async Task<IActionResult> AddTestResult([FromForm] AddTestResultDto testResultDto, IFormFile formFile)
    {
        if (!ModelState.IsValid || formFile == null) return BadRequest(ModelState);

        var testResult = mapper.Map<TestResult>(testResultDto);
        var testRequest = await testRequestService.GetTestRequestByIdAsync(testResult.TestRequestId);
        if (testRequest == null) return NotFound($"Test request with id {testResult.TestRequestId} not found");

        var validTestTypeIds = testRequest.TestTypes.Select(tt => tt.Id).ToList();
        var invalidTestTypeIds = testResultDto.TestTypesIds.Except(validTestTypeIds).ToList();
        if (invalidTestTypeIds.Any()) return BadRequest($"The following test type IDs are not valid for this test request: {string.Join(", ", invalidTestTypeIds)}");


        var createdTestResult = await testResultService.UploadResult(testResult, testRequest, formFile);

        try
        {
            var testTypes = await testTypeService.GetTestTypesFromIdList(testResultDto.TestTypesIds);
            createdTestResult = await testResultService.AddTestTypesAsync(createdTestResult, testTypes);
            return Ok(mapper.Map<TestResultDto>(createdTestResult));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{testResultId}")]
    public async Task<IActionResult> DeleteTestResult([FromRoute] int testResultId)
    {
        var result = await testResultService.DeleteTestResultAsync(testResultId);
        if (!result) return NotFound($"Test result with id {testResultId} not found");

        return Ok();
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


        if (user.Role.Name == "Patient" && testResult.TestRequest.PatientId != user.Id) return Unauthorized();
        if (user.Role.Name == "Doctor" && testResult.TestRequest.DoctorId != user.Id) return Unauthorized();


        var pdfPath = testResult.FilePath;
        var pdf = await fileService.DownloadFile(pdfPath);
        return File(pdf.Item1, pdf.Item2, pdf.Item3);
    }
}