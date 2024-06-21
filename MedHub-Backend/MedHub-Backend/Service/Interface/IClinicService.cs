using MedHub_Backend.Model;

namespace MedHub_Backend.Service.Interface;

public interface IClinicService
{
    IQueryable<Clinic> GetAllClinics();
    Task<Clinic?> GetClinicByIdAsync(int clinicId);
    Task<IEnumerable<User>> GetAllPatientsOfClinicAsync(int clinicId);
    Task<Clinic> CreateClinicAsync(Clinic clinic);
    Task<Clinic> UpdateClinicAsync(Clinic clinic);
    Task<bool> DeleteClinicByIdAsync(int clinicId);
}