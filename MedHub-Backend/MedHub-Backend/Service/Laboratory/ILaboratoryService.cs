namespace MedHub_Backend.Service.Laboratory;

public interface ILaboratoryService
{
    Task<List<Model.Laboratory>> GetAllLaboratoriesAsync();
    Task<Model.Laboratory?> GetLaboratoryByIdAsync(int laboratoryId);
    Task<Model.Laboratory> CreateLaboratoryAsync(Model.Laboratory laboratory, List<Model.TestType> testTypes);
    Task<bool> DeleteLaboratoryAsync(int laboratoryId);
    Task<Model.Laboratory> UpdateLaboratoryAsync(Model.Laboratory laboratory);
}