using Medhub_Backend.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Medhub_Backend.Application.Abstractions.Service;

public interface ITestResultService
{
    IQueryable<TestResult> GetAllAsync();
    Task<TestResult?> GetByIdAsync(int testResultId);
    Task DeleteByIdAsync(int testResultId);
    Task<TestResult> CreateTestResultWithFile(TestResult testResult, List<int> testTypeIds, TestRequest testRequest, IFormFile formFile);
    Task<(byte[], string, string)?> DownloadTestResultPdf(int resultId);
}