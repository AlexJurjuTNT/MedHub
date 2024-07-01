using Medhub_Backend.Application.Abstractions.Persistence;
using Medhub_Backend.Domain.Entities;
using Medhub_Backend.Infrastructure.Persistence;

namespace Medhub_Backend.Infrastructure.Repositories;

public class TestRequestRepository : ITestRequestRepository
{
    private readonly AppDbContext _context;

    public TestRequestRepository(AppDbContext context)
    {
        _context = context;
    }

    public IQueryable<TestRequest> GetAllAsync()
    {
        return _context.TestRequests.AsQueryable();
    }

    public async Task<TestRequest?> GetByIdAsync(int testRequestId)
    {
        return await _context.TestRequests.FindAsync(testRequestId);
    }

    public async Task AddAsync(TestRequest testRequest)
    {
        await _context.TestRequests.AddAsync(testRequest);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TestRequest testRequest)
    {
        _context.TestRequests.Update(testRequest);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(TestRequest testRequest)
    {
        _context.TestRequests.Remove(testRequest);
        await _context.SaveChangesAsync();
    }
}