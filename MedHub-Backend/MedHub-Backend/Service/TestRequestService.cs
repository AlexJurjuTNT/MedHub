using MedHub_Backend.Context;
using MedHub_Backend.Model;
using MedHub_Backend.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace MedHub_Backend.Service;

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

    public async Task<TestRequest> AddTestTypesAsync(TestRequest testRequest, List<TestType> testTypes)
    {
        testRequest.TestTypes = testTypes;
        appDbContext.TestRequests.Update(testRequest);
        await appDbContext.SaveChangesAsync();
        return testRequest;
    }

    public async Task<List<TestRequest>> GetAllTestRequestsOfUserAsync(int userId)
    {
        return await appDbContext.TestRequests.Where(t => t.PatientId == userId).ToListAsync();
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