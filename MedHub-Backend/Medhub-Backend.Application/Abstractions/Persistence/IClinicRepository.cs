using Medhub_Backend.Domain.Entities;

namespace Medhub_Backend.Application.Abstractions.Persistence;

public interface IClinicRepository
{
    Task UpdateAsync(Clinic clinic);
    Task AddAsync(Clinic clinic);
    Task<Clinic?> GetByIdAsync(int clinicId);
    IQueryable<Clinic> GetAll();
    void Remove(Clinic clinic);
}