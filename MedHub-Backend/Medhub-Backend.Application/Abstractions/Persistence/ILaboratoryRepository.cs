using Medhub_Backend.Domain.Entities;

namespace Medhub_Backend.Application.Abstractions.Persistence;

public interface ILaboratoryRepository
{
    Task<Laboratory?> GetByIdAsync(int laboratoryId);
    Task AddAsync(Laboratory laboratory);
    void Remove(Laboratory laboratory);
    Task UpdateAsync(Laboratory laboratory);
    IQueryable<Laboratory> GetAll();
}