using MedHub_Backend.Model;

namespace MedHub_Backend.Service.Interface;

public interface ITestResultService
{
    Task<List<TestResult>> GetAllTestResultsAsync();
    Task<TestResult?> GetTestResultByIdAsync(int testResultId);
    Task<TestResult> CreateTestResult(TestResult testResult);
    Task<TestResult> UpdateTestResultAsync(TestResult testResult);
    Task<bool> DeleteTestResult(int testResultId);
}