using MedHub_Backend.Context;
using MedHub_Backend.Helper;
using MedHub_Backend.Service.Email;
using MedHub_Backend.Service.File;
using Microsoft.EntityFrameworkCore;

namespace MedHub_Backend.Service.TestResult;

public class TestResultService(
    AppDbContext appDbContext,
    IFileService fileService,
    IEmailService emailService
) : ITestResultService
{
    public async Task<List<Model.TestResult>> GetAllTestResultsAsync()
    {
        return await appDbContext.TestResults.ToListAsync();
    }

    public async Task<Model.TestResult?> GetTestResultByIdAsync(int testResultId)
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

    public async Task<Model.TestResult> CreateTestResultAsync(Model.TestResult testResult)
    {
        await appDbContext.TestResults.AddAsync(testResult);
        await appDbContext.SaveChangesAsync();
        return testResult;
    }

    public async Task<Model.TestResult> UploadResult(Model.TestResult testResult, Model.TestRequest testRequest, IFormFile formFile)
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

    public async Task<Model.TestResult> AddTestTypesAsync(Model.TestResult testResult, List<Model.TestType> testTypes)
    {
        testResult.TestTypes = testTypes;
        appDbContext.TestResults.Update(testResult);
        await appDbContext.SaveChangesAsync();
        return testResult;
    }

    public async Task<Model.TestResult> UpdateTestResultAsync(Model.TestResult testResult)
    {
        appDbContext.TestResults.Update(testResult);
        await appDbContext.SaveChangesAsync();
        return testResult;
    }

    private async Task<string> UploadResultFile(IFormFile formFile, Model.User patient, Model.Clinic clinic)
    {
        var uploadPath = LocalStorageHelper.GetClinicUserPath(clinic.Name, patient.Username);
        var pdfPath = await fileService.UploadFile(formFile, uploadPath);
        return pdfPath;
    }
}