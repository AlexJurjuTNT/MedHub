using MedHub_Backend.Model;

namespace MedHub_Backend.Service.Interface;

public interface ITestTypeService
{
    Task<List<TestType>> GetAllTestTypesAsync();
    Task<TestType?> GetTestTypeByIdAsync(int testTypeId);
    Task<TestType> CreateTestTypeAsync(TestType testType);
    Task<TestType> UpdateTestTypeAsync(TestType testType);
    Task<bool> DeleteClinicByIdAsync(int testTypeId);
}