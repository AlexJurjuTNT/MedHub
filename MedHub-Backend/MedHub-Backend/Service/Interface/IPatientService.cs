using MedHub_Backend.Model;

namespace MedHub_Backend.Service.Interface;

public interface IPatientService
{
    Task<Patient> CreatePatientAsync(Patient patient);
    Task<Patient> UpdatePatientAsync(Patient patient);
    Task<bool> DeletePatientAsync(int patientId);
    Task<List<Patient>> GetAllPatients();
    Task<Patient?> GetPatientById(int patientId);
}