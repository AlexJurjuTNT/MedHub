using Medhub_Backend.Domain.Model;

namespace Medhub_Backend.Business.Service.Interface;

public interface ITestTypeService
{
    IQueryable<TestType> GetAllTestTypes();
    Task<TestType?> GetTestTypeByIdAsync(int testTypeId);
    Task<TestType> CreateTestTypeAsync(TestType testType);
    Task<TestType> UpdateTestTypeAsync(TestType testType);
    Task<bool> DeleteClinicByIdAsync(int testTypeId);
    Task<List<TestType>> GetTestTypesFromIdList(List<int> testTypesIds);
}