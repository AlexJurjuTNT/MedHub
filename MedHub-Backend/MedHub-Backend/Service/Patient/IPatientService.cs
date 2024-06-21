namespace MedHub_Backend.Service.Patient;

public interface IPatientService
{
    Task<Model.Patient> CreatePatientAsync(Model.Patient patient);
    Task<List<Model.User>> GetAllUserPatientsAsync();
    Task<Model.Patient?> GetPatientAsync(int patientId);
    Task<Model.Patient> UpdatePatientAsync(Model.Patient patient);
    Task<bool> DeletePatientAsync(int patientId);
}