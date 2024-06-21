using MedHub_Backend.Context;
using Microsoft.EntityFrameworkCore;

namespace MedHub_Backend.Service.TestRequest;

public class TestRequestService(
    AppDbContext appDbContext
) : ITestRequestService
{
    public async Task<List<Model.TestRequest>> GetAllTestRequestsAsync()
    {
        return await appDbContext.TestRequests.ToListAsync();
    }

    public async Task<Model.TestRequest?> GetTestRequestByIdAsync(int testRequestId)
    {
        return await appDbContext.TestRequests.FindAsync(testRequestId);
    }

    public async Task<Model.TestRequest> CreateNewTestRequestAsync(Model.TestRequest testRequest, List<Model.TestType> testTypes)
    {
        await appDbContext.TestRequests.AddAsync(testRequest);
        testRequest.TestTypes = testTypes;
        await appDbContext.SaveChangesAsync();
        return testRequest;
    }

    public async Task<Model.TestRequest> UpdateTestRequestAsync(Model.TestRequest testRequest)
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

    public async Task<List<Model.TestRequest>> GetAllTestRequestsOfUserAsync(int userId)
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