using Medhub_Backend.Domain.Entities;

namespace Medhub_Backend.Application.Abstractions.Service;

public interface ITestTypeService
{
    IQueryable<TestType> GetAllAsync();
    Task<TestType?> GetByIdAsync(int testTypeId);
    Task<TestType> CreateAsync(TestType testType);
    Task<TestType> UpdateAsync(TestType testType);
    Task<bool> DeleteByIdAsync(int testTypeId);
    Task<List<TestType>> GetTestTypesFromIdList(List<int> testTypesIds);
}