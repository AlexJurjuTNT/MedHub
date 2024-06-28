using Medhub_Backend.Domain.Entities;

namespace Medhub_Backend.Application.Abstractions.Persistence;

public interface ITestRequestRepository
{
    IQueryable<TestRequest> GetAllAsync();
    Task<TestRequest?> GetByIdAsync(int testRequestId);
    Task AddAsync(TestRequest testRequest);
    Task UpdateAsync(TestRequest testRequest);
    Task RemoveAsync(TestRequest testRequest);
}