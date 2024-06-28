using Medhub_Backend.Domain.Entities;

namespace Medhub_Backend.Application.Abstractions.Persistence;

public interface ITestResultRepository
{
    Task AddAsync(TestResult testResult);
    Task UpdateAsync(TestResult testResult);
    void Remove(object testResult);
    Task<TestResult?> GetByIdAsync(int testResultId);
    IQueryable<TestResult> GetAll();
}