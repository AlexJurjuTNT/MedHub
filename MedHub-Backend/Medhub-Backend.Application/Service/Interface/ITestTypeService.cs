using Medhub_Backend.Domain.Entities;

namespace Medhub_Backend.Application.Service.Interface;

public interface ITestTypeService
{
    IQueryable<TestType> GetAllTestTypesAsync();
    Task<TestType?> GetTestTypeByIdAsync(int testTypeId);
    Task<TestType> CreateTestTypeAsync(TestType testType);
    Task<TestType> UpdateTestTypeAsync(TestType testType);
    Task<bool> DeleteTestTypeByIdAsync(int testTypeId);
    Task<List<TestType>> GetTestTypesFromIdList(List<int> testTypesIds);
}