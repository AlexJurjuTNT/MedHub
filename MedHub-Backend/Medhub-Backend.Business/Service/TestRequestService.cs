using Medhub_Backend.Business.Service.Interface;
using Medhub_Backend.DataAccess.Persistence;
using Medhub_Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Medhub_Backend.Business.Service;

public class TestRequestService : ITestRequestService
{
    private readonly AppDbContext _appDbContext;
    private readonly IEmailService _emailService;

    public TestRequestService(AppDbContext appDbContext, IEmailService emailService)
    {
        _appDbContext = appDbContext;
        _emailService = emailService;
    }

    public async Task<List<TestRequest>> GetAllTestRequestsAsync()
    {
        return await _appDbContext.TestRequests.ToListAsync();
    }

    public async Task<TestRequest?> GetTestRequestByIdAsync(int testRequestId)
    {
        return await _appDbContext.TestRequests.FindAsync(testRequestId);
    }

    public async Task<TestRequest> CreateNewTestRequestAsync(TestRequest testRequest, Clinic clinic)
    {
        await _appDbContext.TestRequests.AddAsync(testRequest);
        await _appDbContext.SaveChangesAsync();

        await _emailService.SendCreatedTestRequestEmail(clinic, testRequest);

        return testRequest;
    }

    public async Task<TestRequest> UpdateTestRequestAsync(TestRequest testRequest)
    {
        _appDbContext.TestRequests.Update(testRequest);
        await _appDbContext.SaveChangesAsync();
        return testRequest;
    }

    public async Task<bool> DeleteTestRequestAsync(int testRequestId)
    {
        var testRequest = await _appDbContext.TestRequests.FindAsync(testRequestId);
        if (testRequest == null) return false;

        _appDbContext.TestRequests.Remove(testRequest);
        await _appDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<List<TestRequest>> GetAllTestRequestsOfUserAsync(int userId)
    {
        return await _appDbContext.TestRequests.Where(t => t.PatientId == userId).ToListAsync();
    }

    public IQueryable<TestRequest> GetAllTestRequestsOfUserInClinicAsync(int userId, int clinicId)
    {
        return _appDbContext.TestRequests.Where(t => t.PatientId == userId && t.ClinicId == clinicId);
    }

    public async Task<List<TestType>> GetRemainingTestTypesAsync(int testRequestId)
    {
        return await _appDbContext.TestRequests
            .Where(tr => tr.Id == testRequestId)
            .SelectMany(tr => tr.TestTypes.Where(tt => !tr.TestResults.Any(r => r.TestTypes.Contains(tt))))
            .ToListAsync();
    }
}