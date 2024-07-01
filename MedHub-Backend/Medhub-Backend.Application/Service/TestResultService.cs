using Medhub_Backend.Application.Abstractions.Persistence;
using Medhub_Backend.Application.Abstractions.Service;
using Medhub_Backend.Application.Helper;
using Medhub_Backend.Domain.Entities;
using Medhub_Backend.Domain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Medhub_Backend.Application.Service;

public class TestResultService : ITestResultService
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IEmailService _emailService;
    private readonly IFileService _fileService;
    private readonly ITestRequestService _testRequestService;
    private readonly ITestResultRepository _testResultRepository;
    private readonly ITestTypeService _testTypeService;

    public TestResultService(
        ITestResultRepository testResultRepository,
        IFileService fileService,
        IEmailService emailService,
        ITestTypeService testTypeService,
        IDateTimeProvider dateTimeProvider, ITestRequestService testRequestService)
    {
        _testResultRepository = testResultRepository;
        _fileService = fileService;
        _emailService = emailService;
        _testTypeService = testTypeService;
        _dateTimeProvider = dateTimeProvider;
        _testRequestService = testRequestService;
    }

    public IQueryable<TestResult> GetAllAsync()
    {
        return _testResultRepository.GetAll();
    }

    public async Task<TestResult?> GetByIdAsync(int testResultId)
    {
        return await _testResultRepository.GetByIdAsync(testResultId);
    }

    public async Task DeleteByIdAsync(int testResultId)
    {
        var testResult = await _testResultRepository.GetByIdAsync(testResultId);
        if (testResult is null) throw new TestResultNotFoundException(testResultId);
        await _testResultRepository.Remove(testResult);
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
        var testResult = await GetByIdAsync(resultId);
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
        await _testRequestService.UpdateAsync(testRequest);

        return createdTestResult;
    }

    private async Task<TestResult> CreateTestResultAsync(TestResult testResult)
    {
        await _testResultRepository.AddAsync(testResult);
        return testResult;
    }

    private async Task<TestResult> AddTestTypesAsync(TestResult testResult, List<TestType> testTypes)
    {
        testResult.TestTypes = testTypes;
        await _testResultRepository.UpdateAsync(testResult);
        return testResult;
    }

    private async Task<string> UploadResultFile(IFormFile formFile, User patient, Clinic clinic)
    {
        return await _fileService.UploadFile(formFile, clinic.Name, patient.Username);
    }
}