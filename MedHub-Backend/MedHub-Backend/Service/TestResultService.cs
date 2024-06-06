using MedHub_Backend.Data;
using MedHub_Backend.Model;
using MedHub_Backend.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace MedHub_Backend.Service;

public class TestResultService(
    AppDbContext appDbContext,
    IFileService fileService
) : ITestResultService
{
    public async Task<List<TestResult>> GetAllTestResultsAsync()
    {
        return await appDbContext.TestResults.ToListAsync();
    }

    public async Task<TestResult?> GetTestResultByIdAsync(int testResultId)
    {
        return await appDbContext.TestResults.FindAsync(testResultId);
    }

    // todo: upload file
    // todo: email patient
    public async Task<TestResult> CreateTestResult(TestResult testResult)
    {
        await appDbContext.TestResults.AddAsync(testResult);
        await appDbContext.SaveChangesAsync();
        return testResult;
    }

    public async Task<TestResult> UpdateTestResultAsync(TestResult testResult)
    {
        appDbContext.TestResults.Update(testResult);
        await appDbContext.SaveChangesAsync();
        return testResult;
    }

    public async Task<bool> DeleteTestResult(int testResultId)
    {
        var testResult = await appDbContext.TestResults.FindAsync(testResultId);
        if (testResult == null) return false;

        appDbContext.TestResults.Remove(testResult);
        await appDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<TestResult> UploadFile(TestResult testResult, IFormFile formFile)
    {
        string pdfPath = await fileService.UploadFile(formFile);
        testResult.CompletionDate = DateTime.UtcNow;
        testResult.FilePath = pdfPath;
        return await CreateTestResult(testResult);
    }
}