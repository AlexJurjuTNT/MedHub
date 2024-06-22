using Medhub_Backend.Domain.Model;
using Microsoft.AspNetCore.Http;

namespace Medhub_Backend.Business.Service.Interface;

public interface ITestResultService
{
    Task<List<TestResult>> GetAllTestResultsAsync();
    Task<TestResult?> GetTestResultByIdAsync(int testResultId);
    Task<bool> DeleteTestResultAsync(int testResultId);
    Task<TestResult> CreateTestResultAsync(TestResult testResult);
    Task<TestResult> UploadResult(TestResult testResult, TestRequest testRequest, IFormFile formFile);
    Task<TestResult> AddTestTypesAsync(TestResult createdTestResult, List<TestType> testTypes);
}