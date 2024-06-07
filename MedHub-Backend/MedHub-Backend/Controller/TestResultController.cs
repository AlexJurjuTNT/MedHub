using AutoMapper;
using MedHub_Backend.Dto;
using MedHub_Backend.Model;
using MedHub_Backend.Service;
using MedHub_Backend.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MedHub_Backend.Controller;

[ApiController]
[Route("api/v1/[controller]")]
public class TestResultController(
    ITestResultService testResultService,
    IFileService fileService,
    ITestRequestService testRequestService,
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

    [HttpGet("{resultId}")]
    [ProducesResponseType(200, Type = typeof(FileResult))]
    public async Task<IActionResult> DownloadPdf([FromRoute] int resultId)
    {
        var testResult = await testResultService.GetTestResultByIdAsync(resultId);
        if (testResult == null) return NotFound($"Result with id {resultId} not found");

        string pdfPath = testResult.FilePath;
        var pdf = await fileService.DownloadFile(pdfPath);
        return File(pdf.Item1, pdf.Item2, pdf.Item3);
    }
}