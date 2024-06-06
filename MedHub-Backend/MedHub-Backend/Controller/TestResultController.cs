using AutoMapper;
using MedHub_Backend.Dto;
using MedHub_Backend.Model;
using MedHub_Backend.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MedHub_Backend.Controller;

[ApiController]
[Route("api/v1/[controller]")]
public class TestResultController(
    ITestResultService testResultService,
    IMapper mapper
) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Upload([FromForm] AddTestResultDto testResultDto, IFormFile formFile)
    {
        // todo: make it so a file is uploaded to a separate folder for each clinic
        TestResult testResult = mapper.Map<TestResult>(testResultDto);
        TestResult result = await testResultService.UploadFile(testResult, formFile);
        return Ok(result);
    }
}