namespace MedHub_Backend.Service.TestType;

public interface ITestTypeService
{
    Task<List<Model.TestType>> GetAllTestTypesAsync();
    Task<Model.TestType?> GetTestTypeByIdAsync(int testTypeId);
    Task<Model.TestType> CreateTestTypeAsync(Model.TestType testType);
    Task<Model.TestType> UpdateTestTypeAsync(Model.TestType testType);
    Task<bool> DeleteClinicByIdAsync(int testTypeId);
    Task<List<Model.TestType>> GetTestTypesFromIdList(List<int> testTypesIds);
}