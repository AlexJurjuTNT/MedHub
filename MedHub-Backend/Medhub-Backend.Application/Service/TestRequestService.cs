using Medhub_Backend.Application.Abstractions.Persistence;
using Medhub_Backend.Application.Abstractions.Service;
using Medhub_Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Medhub_Backend.Application.Service;

public class TestRequestService : ITestRequestService
{
    private readonly IEmailService _emailService;
    private readonly ITestRequestRepository _testRequestRepository;

    public TestRequestService(ITestRequestRepository testRequestRepository, IEmailService emailService)
    {
        _testRequestRepository = testRequestRepository;
        _emailService = emailService;
    }

    public IQueryable<TestRequest> GetAllAsync()
    {
        return _testRequestRepository.GetAllAsync();
    }

    public async Task<TestRequest?> GetByIdAsync(int testRequestId)
    {
        return await _testRequestRepository.GetByIdAsync(testRequestId);
    }

    public async Task<TestRequest> CreateAsync(TestRequest testRequest, Clinic clinic)
    {
        await _testRequestRepository.AddAsync(testRequest);
        await _emailService.SendCreatedTestRequestEmail(clinic, testRequest);
        return testRequest;
    }

    public async Task<TestRequest> UpdateAsync(TestRequest testRequest)
    {
        await _testRequestRepository.UpdateAsync(testRequest);
        return testRequest;
    }

    public async Task<bool> DeleteAsync(int testRequestId)
    {
        var testRequest = await _testRequestRepository.GetByIdAsync(testRequestId);
        if (testRequest == null) return false;

        await _testRequestRepository.RemoveAsync(testRequest);
        return true;
    }

    public async Task<List<TestRequest>> GetAllTestRequestsOfUserAsync(int userId)
    {
        return await _testRequestRepository.GetAllAsync()
            .Where(t => t.PatientId == userId)
            .ToListAsync();
    }

    public IQueryable<TestRequest> GetAllTestRequestsOfUserInClinicAsync(int userId, int clinicId)
    {
        return _testRequestRepository.GetAllAsync()
            .Where(t => t.PatientId == userId && t.ClinicId == clinicId);
    }

    public async Task<List<TestType>> GetRemainingTestTypesAsync(int testRequestId)
    {
        return await _testRequestRepository.GetAllAsync()
            .Where(tr => tr.Id == testRequestId)
            .SelectMany(tr => tr.TestTypes.Where(tt => !tr.TestResults.Any(r => r.TestTypes.Contains(tt))))
            .ToListAsync();
    }
}