using Medhub_Backend.Domain.Entities;

namespace Medhub_Backend.Application.Abstractions.Persistence;

public interface IPatientRepository
{
    IQueryable<Patient> GetAllAsync();
    Task<Patient?> GetByIdAsync(int patientId);
    Task AddAsync(Patient patient);
    Task UpdateAsync(Patient patient);
    Task RemoveAsync(Patient patient);
}