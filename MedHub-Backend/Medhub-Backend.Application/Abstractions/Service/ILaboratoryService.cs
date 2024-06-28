using Medhub_Backend.Domain.Entities;

namespace Medhub_Backend.Application.Abstractions.Service;

public interface ILaboratoryService
{
    IQueryable<Laboratory> GetAllAsync();
    Task<Laboratory?> GetByIdAsync(int laboratoryId);
    Task<Laboratory> CreateAsync(Laboratory laboratory, List<TestType> testTypes);
    Task<bool> DeleteAsync(int laboratoryId);
    Task<Laboratory> UpdateAsync(Laboratory laboratory);
}