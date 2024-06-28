using Medhub_Backend.Domain.Entities;

namespace Medhub_Backend.Application.Abstractions.Persistence;

public interface ITestTypeRepository
{
    IQueryable<TestType> GetAllAsync();
    Task<TestType?> GetByIdAsync(int testTypeId);
    Task AddAsync(TestType testType);
    Task UpdateAsync(TestType testType);
    Task RemoveAsync(TestType testType);
}