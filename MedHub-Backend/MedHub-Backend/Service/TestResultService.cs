using MedHub_Backend.Context;
using MedHub_Backend.Helper;
using MedHub_Backend.Model;
using MedHub_Backend.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace MedHub_Backend.Service;

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

    public async Task<TestResult> UpdateTestResultAsync(TestResult testResult)
    {
        appDbContext.TestResults.Update(testResult);
        await appDbContext.SaveChangesAsync();
        return testResult;
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
        // interesting detail - when i return this TestResults object, the testRequest that comes with it is null because the db is not accessed to update the fields
        return testResult;
    }

    public async Task<TestResult> UploadResult(TestResult testResult, TestRequest testRequest, IFormFile formFile)
    {
        // upload the pdf
        var patient = testRequest.Patient;
        var clinic = patient.Clinic;
        var pdfPath = await UploadResultFile(formFile, patient, clinic);

        // insert testResult object to the db
        testResult.FilePath = pdfPath;
        testResult.CompletionDate = DateTime.UtcNow;
        var createdTestResult = await CreateTestResultAsync(testResult);

        await emailService.SendPatientResultsCompleteEmail(clinic.SendgridApiKey, patient.Email, patient.Username);

        return createdTestResult;
    }

    private async Task<string> UploadResultFile(IFormFile formFile, User patient, Clinic clinic)
    {
        var uploadPath = LocalStorageHelper.GetClinicUserPath(clinic.Name, patient.Username);
        var pdfPath = await fileService.UploadFile(formFile, uploadPath);
        return pdfPath;
    }
}