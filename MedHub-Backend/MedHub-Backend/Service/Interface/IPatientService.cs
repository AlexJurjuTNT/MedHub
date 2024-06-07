using MedHub_Backend.Model;

namespace MedHub_Backend.Service.Interface;

public interface IPatientService
{
    Task<Patient> CreatePatientAsync(Patient patient);
    Task<List<User>> GetAllPatientsAsync();
    Task<Patient?> GetPatientAsync(int patientId);
    Task<Patient> UpdatePatientAsync(Patient patient);
    Task<bool> DeletePatientAsync(int patientId);
}