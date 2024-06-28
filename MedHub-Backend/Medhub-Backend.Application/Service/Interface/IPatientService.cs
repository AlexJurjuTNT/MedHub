using Medhub_Backend.Domain.Entities;

namespace Medhub_Backend.Application.Service.Interface;

public interface IPatientService
{
    Task<Patient> CreatePatientAsync(Patient patient);
    IQueryable<User> GetAllUserPatientsAsync();
    Task<Patient?> GetPatientAsync(int patientId);
    Task<Patient> UpdatePatientAsync(Patient patient);
    Task<bool> DeletePatientAsync(int patientId);
}