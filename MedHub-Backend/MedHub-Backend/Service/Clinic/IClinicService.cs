namespace MedHub_Backend.Service.Clinic;

public interface IClinicService
{
    Task<List<Model.Clinic>> GetAllClinicsAsync();
    Task<Model.Clinic?> GetClinicByIdAsync(int clinicId);
    Task<IEnumerable<Model.User>> GetAllPatientsOfClinicAsync(int clinicId);
    Task<Model.Clinic> CreateClinicAsync(Model.Clinic clinic);
    Task<Model.Clinic> UpdateClinicAsync(Model.Clinic clinic);
    Task<bool> DeleteClinicByIdAsync(int clinicId);
}