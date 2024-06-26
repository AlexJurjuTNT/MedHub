using Medhub_Backend.Domain.Model;
using Microsoft.AspNetCore.Http;

namespace Medhub_Backend.Business.Service.Interface;

public interface ITestResultService
{
    Task<List<TestResult>> GetAllTestResultsAsync();
    Task<TestResult?> GetTestResultByIdAsync(int testResultId);
    Task<bool> DeleteTestResultAsync(int testResultId);
    Task<TestResult> CreateTestResultWithFile(TestResult testResult, List<int> testTypeIds, TestRequest testRequest, IFormFile formFile);
    Task<(byte[], string, string)?> DownloadTestResultPdf(int resultId);
}