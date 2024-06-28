using Medhub_Backend.Application.Abstractions.Persistence;
using Medhub_Backend.Application.Abstractions.Service;
using Medhub_Backend.Domain.Entities;
using Medhub_Backend.Domain.Exceptions;

namespace Medhub_Backend.Application.Service;

public class ClinicService : IClinicService
{
    private readonly IClinicRepository _clinicRepository;

    public ClinicService(IClinicRepository clinicRepository)
    {
        _clinicRepository = clinicRepository;
    }


    public IQueryable<Clinic> GetAll()
    {
        return _clinicRepository.GetAll();
    }

    public async Task<Clinic?> GetByIdAsync(int clinicId)
    {
        return await _clinicRepository.GetByIdAsync(clinicId);
    }

    public async Task<IEnumerable<User>> GetAllPatientsOfClinicAsync(int clinicId)
    {
        var clinic = await GetByIdAsync(clinicId);
        if (clinic == null) throw new ClinicNotFoundException(clinicId);

        var patients = clinic.Users.Where(u => u.Role.Name == "Patient");
        return patients;
    }

    public async Task<Clinic> CreateAsync(Clinic clinic)
    {
        await _clinicRepository.AddAsync(clinic);
        return clinic;
    }

    public async Task<Clinic> UpdateAsync(Clinic clinic)
    {
        await _clinicRepository.UpdateAsync(clinic);
        return clinic;
    }

    public async Task<bool> DeleteByIdAsync(int clinicId)
    {
        var clinic = await _clinicRepository.GetByIdAsync(clinicId);
        if (clinic == null) return false;

        _clinicRepository.Remove(clinic);
        return true;
    }
}