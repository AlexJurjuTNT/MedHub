using Medhub_Backend.Application.Abstractions.Persistence;
using Medhub_Backend.Domain.Entities;
using Medhub_Backend.Infrastructure.Persistence;

namespace Medhub_Backend.Infrastructure.Repositories;

public class TestTypeRepository : ITestTypeRepository
{
    private readonly AppDbContext _context;

    public TestTypeRepository(AppDbContext context)
    {
        _context = context;
    }

    public IQueryable<TestType> GetAllAsync()
    {
        return _context.TestTypes.AsQueryable();
    }

    public async Task<TestType?> GetByIdAsync(int testTypeId)
    {
        return await _context.TestTypes.FindAsync(testTypeId);
    }

    public async Task AddAsync(TestType testType)
    {
        await _context.TestTypes.AddAsync(testType);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TestType testType)
    {
        _context.TestTypes.Update(testType);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(TestType testType)
    {
        _context.TestTypes.Remove(testType);
        await _context.SaveChangesAsync();
    }
}