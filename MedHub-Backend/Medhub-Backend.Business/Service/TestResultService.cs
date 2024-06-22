using Medhub_Backend.Business.Helper;
using Medhub_Backend.Business.Service.Interface;
using Medhub_Backend.DataAccess.Persistence;
using Medhub_Backend.Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Medhub_Backend.Business.Service;

public class TestResultService(
    AppDbContext appDbContext,
    IFileService fileService,
    IEmailService emailService
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

    public async Task<bool> DeleteTestResultAsync(int testResultId)
    {
        var testResult = await appDbContext.TestResults.FindAsync(testResultId);
        if (testResult == null) return false;

        appDbContext.TestResults.Remove(testResult);
        await appDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<TestResult> CreateTestResultAsync(TestResult testResult)
    {
        await appDbContext.TestResults.AddAsync(testResult);
        await appDbContext.SaveChangesAsync();
        return testResult;
    }

    public async Task<TestResult> UploadResult(TestResult testResult, TestRequest testRequest, IFormFile formFile)
    {
        // upload the pdf
        var patientUser = testRequest.Patient;
        var clinic = patientUser.Clinic;
        var pdfPath = await UploadResultFile(formFile, patientUser, clinic);

        // insert testResult object to the db
        testResult.FilePath = pdfPath;
        testResult.CompletionDate = DateTime.UtcNow;
        var createdTestResult = await CreateTestResultAsync(testResult);

        await emailService.SendPatientResultsCompleteEmail(clinic, patientUser);

        return createdTestResult;
    }

    public async Task<TestResult> AddTestTypesAsync(TestResult testResult, List<TestType> testTypes)
    {
        testResult.TestTypes = testTypes;
        appDbContext.TestResults.Update(testResult);
        await appDbContext.SaveChangesAsync();
        return testResult;
    }

    public async Task<TestResult> UpdateTestResultAsync(TestResult testResult)
    {
        appDbContext.TestResults.Update(testResult);
        await appDbContext.SaveChangesAsync();
        return testResult;
    }

    private async Task<string> UploadResultFile(IFormFile formFile, User patient, Clinic clinic)
    {
        var uploadPath = LocalStorageHelper.GetClinicUserPath(clinic.Name, patient.Username);
        var pdfPath = await fileService.UploadFile(formFile, uploadPath);
        return pdfPath;
    }
}