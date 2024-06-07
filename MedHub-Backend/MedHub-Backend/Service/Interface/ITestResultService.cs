using MedHub_Backend.Model;

namespace MedHub_Backend.Service.Interface;

public interface ITestResultService
{
    Task<List<TestResult>> GetAllTestResultsAsync();
    Task<TestResult?> GetTestResultByIdAsync(int testResultId);
    Task<TestResult> UpdateTestResultAsync(TestResult testResult);
    Task<bool> DeleteTestResult(int testResultId);
    Task<TestResult> CreateTestResultAsync(TestResult testResult);
    Task<TestResult> UploadResult(TestResult testResult, IFormFile formFile);
}