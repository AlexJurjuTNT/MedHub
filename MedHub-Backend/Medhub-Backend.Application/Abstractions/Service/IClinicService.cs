using Medhub_Backend.Domain.Entities;

namespace Medhub_Backend.Application.Abstractions.Service;

public interface IClinicService
{
    IQueryable<Clinic> GetAll();
    Task<Clinic?> GetByIdAsync(int clinicId);
    Task<IEnumerable<User>> GetAllPatientsOfClinicAsync(int clinicId);
    Task<Clinic> CreateAsync(Clinic clinic);
    Task<Clinic> UpdateAsync(Clinic clinic);
    Task<bool> DeleteByIdAsync(int clinicId);
}