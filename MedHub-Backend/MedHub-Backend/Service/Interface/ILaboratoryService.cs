using MedHub_Backend.Model;

namespace MedHub_Backend.Service.Interface;

public interface ILaboratoryService
{
    Task<List<Laboratory>> GetAllLaboratoriesAsync();
    Task<Laboratory?> GetLaboratoryByIdAsync(int laboratoryId);
    Task<Laboratory> CreateLaboratoryAsync(Laboratory laboratory, List<TestType> testTypes);
    Task<bool> DeleteLaboratoryAsync(int laboratoryId);
    Task<Laboratory> UpdateLaboratoryAsync(Laboratory laboratory);
}