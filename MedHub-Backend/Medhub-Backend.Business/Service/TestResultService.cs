using Medhub_Backend.Business.Helper;
using Medhub_Backend.Business.Service.Interface;
using Medhub_Backend.DataAccess.Persistence;
using Medhub_Backend.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Medhub_Backend.Business.Service;

public class TestResultService : ITestResultService
{
    private readonly AppDbContext _appDbContext;
    private readonly IEmailService _emailService;
    private readonly IFileService _fileService;
    private readonly ITestTypeService _testTypeService;
    private readonly IDateTimeProvider _dateTimeProvider;

    public TestResultService(
        AppDbContext appDbContext,
        IFileService fileService,
        IEmailService emailService,
        ITestTypeService testTypeService, IDateTimeProvider dateTimeProvider)
    {
        _appDbContext = appDbContext;
        _fileService = fileService;
        _emailService = emailService;
        _testTypeService = testTypeService;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<List<TestResult>> GetAllTestResultsAsync()
    {
        return await _appDbContext.TestResults.ToListAsync();
    }

    public async Task<TestResult?> GetTestResultByIdAsync(int testResultId)
    {
        return await _appDbContext.TestResults.FindAsync(testResultId);
    }

    public async Task<bool> DeleteTestResultAsync(int testResultId)
    {
        var testResult = await _appDbContext.TestResults.FindAsync(testResultId);
        if (testResult == null) return false;

        _appDbContext.TestResults.Remove(testResult);
        await _appDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<TestResult> CreateTestResultWithFile(TestResult testResult, List<int> testTypeIds, TestRequest testRequest, IFormFile formFile)
    {
        var createdTestResult = await UploadResult(testResult, testRequest, formFile);

        var testTypes = await _testTypeService.GetTestTypesFromIdList(testTypeIds);
        createdTestResult = await AddTestTypesAsync(createdTestResult, testTypes);

        return createdTestResult;
    }

    public async Task<(byte[], string, string)?> DownloadTestResultPdf(int resultId)
    {
        var testResult = await GetTestResultByIdAsync(resultId);
        if (testResult == null) return null;
        return await _fileService.DownloadFile(testResult.FilePath);
    }

    private async Task<TestResult> UploadResult(TestResult testResult, TestRequest testRequest, IFormFile formFile)
    {
        var patientUser = testRequest.Patient;
        var clinic = patientUser.Clinic;
        var pdfPath = await UploadResultFile(formFile, patientUser, clinic);

        testResult.FilePath = pdfPath;
        testResult.CompletionDate = _dateTimeProvider.UtcNow;
        var createdTestResult = await CreateTestResultAsync(testResult);

        await _emailService.SendPatientResultsCompleteEmail(clinic, patientUser);

        return createdTestResult;
    }

    private async Task<TestResult> CreateTestResultAsync(TestResult testResult)
    {
        await _appDbContext.TestResults.AddAsync(testResult);
        await _appDbContext.SaveChangesAsync();
        return testResult;
    }

    private async Task<TestResult> AddTestTypesAsync(TestResult testResult, List<TestType> testTypes)
    {
        testResult.TestTypes = testTypes;
        _appDbContext.TestResults.Update(testResult);
        await _appDbContext.SaveChangesAsync();
        return testResult;
    }

    private async Task<string> UploadResultFile(IFormFile formFile, User patient, Clinic clinic)
    {
        var uploadPath = LocalStorageHelper.GetClinicUserPath(clinic.Name, patient.Username);
        return await _fileService.UploadFile(formFile, uploadPath);
    }
}