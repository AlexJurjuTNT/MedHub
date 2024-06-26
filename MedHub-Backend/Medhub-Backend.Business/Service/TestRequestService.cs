using Medhub_Backend.Business.Service.Interface;
using Medhub_Backend.DataAccess.Persistence;
using Medhub_Backend.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Medhub_Backend.Business.Service;

public class TestRequestService(
    AppDbContext appDbContext
) : ITestRequestService
{
    public async Task<List<TestRequest>> GetAllTestRequestsAsync()
    {
        return await appDbContext.TestRequests.ToListAsync();
    }

    public async Task<TestRequest?> GetTestRequestByIdAsync(int testRequestId)
    {
        return await appDbContext.TestRequests.FindAsync(testRequestId);
    }

    public async Task<TestRequest> CreateNewTestRequestAsync(TestRequest testRequest)
    {
        await appDbContext.TestRequests.AddAsync(testRequest);
        await appDbContext.SaveChangesAsync();
        return testRequest;
    }

    public async Task<TestRequest> UpdateTestRequestAsync(TestRequest testRequest)
    {
        appDbContext.TestRequests.Update(testRequest);
        await appDbContext.SaveChangesAsync();
        return testRequest;
    }

    public async Task<bool> DeleteTestRequestAsync(int testRequestId)
    {
        var testRequest = await appDbContext.TestRequests.FindAsync(testRequestId);
        if (testRequest == null) return false;

        appDbContext.TestRequests.Remove(testRequest);
        await appDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<List<TestRequest>> GetAllTestRequestsOfUserAsync(int userId)
    {
        return await appDbContext.TestRequests.Where(t => t.PatientId == userId).ToListAsync();
    }

    public async Task<List<TestRequest>> GetAllTestRequestsOfUserInClinicAsync(int userId, int clinicId)
    {
        return await appDbContext.TestRequests.Where(
                t => t.PatientId == userId && t.ClinicId == clinicId)
            .ToListAsync();
    }

    public async Task<List<int>> GetExistingTestTypeIdsForTestRequestAsync(int testRequestId)
    {
        return await appDbContext.TestResults
            .Where(tr => tr.TestRequestId == testRequestId)
            .SelectMany(tr => tr.TestTypes)
            .Select(tt => tt.Id)
            .Distinct()
            .ToListAsync();
    }
}