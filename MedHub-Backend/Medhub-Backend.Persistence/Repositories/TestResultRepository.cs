using Medhub_Backend.Application.Abstractions.Persistence;
using Medhub_Backend.Domain.Entities;
using Medhub_Backend.Persistence.Persistence;

namespace Medhub_Backend.Persistence.Repositories;

public class TestResultRepository : ITestResultRepository
{
    private readonly AppDbContext _context;

    public TestResultRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(TestResult testResult)
    {
        await _context.TestResults.AddAsync(testResult);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TestResult testResult)
    {
        _context.TestResults.Update(testResult);
        await _context.SaveChangesAsync();
    }

    public void Remove(object testResult)
    {
        _context.TestResults.Remove((TestResult)testResult);
        _context.SaveChanges();
    }

    public async Task<TestResult?> GetByIdAsync(int testResultId)
    {
        return await _context.TestResults.FindAsync(testResultId);
    }

    public IQueryable<TestResult> GetAll()
    {
        return _context.TestResults.AsQueryable();
    }
}