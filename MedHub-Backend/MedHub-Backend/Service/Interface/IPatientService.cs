using MedHub_Backend.Model;

namespace MedHub_Backend.Service.Interface;

public interface IPatientService
{
    Task<Patient> CreatePatientAsync(Patient patient);
    Task<List<Patient>> GetAllPatientsAsync();
    Task<Patient> UpdatePatientAsync(Patient patient);
    Task<bool> DeletePatientAsync(int patientId);
}