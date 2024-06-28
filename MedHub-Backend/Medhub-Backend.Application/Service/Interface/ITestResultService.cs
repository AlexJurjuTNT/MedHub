using Medhub_Backend.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Medhub_Backend.Application.Service.Interface;

public interface ITestResultService
{
    IQueryable<TestResult> GetAllTestResultsAsync();
    Task<TestResult?> GetTestResultByIdAsync(int testResultId);
    Task<bool> DeleteTestResultAsync(int testResultId);
    Task<TestResult> CreateTestResultWithFile(TestResult testResult, List<int> testTypeIds, TestRequest testRequest, IFormFile formFile);
    Task<(byte[], string, string)?> DownloadTestResultPdf(int resultId);
}