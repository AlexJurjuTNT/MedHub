using Medhub_Backend.Domain.Entities;

namespace Medhub_Backend.Application.Abstractions.Service;

public interface IPatientService
{
    Task<Patient> CreateAsync(Patient patient);
    IQueryable<User> GetAllUserPatientsAsync();
    Task<Patient?> GetByIdAsync(int patientId);
    Task<Patient> UpdateAsync(Patient patient);
    Task<bool> DeleteAsync(int patientId);
}