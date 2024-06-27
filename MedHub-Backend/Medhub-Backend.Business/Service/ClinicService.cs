using Medhub_Backend.Business.Service.Interface;
using Medhub_Backend.DataAccess.Persistence;
using Medhub_Backend.Domain.Entities;
using Medhub_Backend.Domain.Exceptions;

namespace Medhub_Backend.Business.Service;

public class ClinicService : IClinicService
{
    private readonly AppDbContext _appDbContext;

    public ClinicService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public IQueryable<Clinic> GetAllClinics()
    {
        return _appDbContext.Clinics;
    }

    public async Task<Clinic?> GetClinicByIdAsync(int clinicId)
    {
        return await _appDbContext.Clinics.FindAsync(clinicId);
    }

    public async Task<IEnumerable<User>> GetAllPatientsOfClinicAsync(int clinicId)
    {
        var clinic = await GetClinicByIdAsync(clinicId);
        if (clinic == null) throw new ClinicNotFoundException(clinicId);

        var patients = clinic.Users.Where(u => u.Role.Name == "Patient");
        return patients;
    }

    public async Task<Clinic> CreateClinicAsync(Clinic clinic)
    {
        await _appDbContext.Clinics.AddAsync(clinic);
        await _appDbContext.SaveChangesAsync();
        return clinic;
    }

    public async Task<Clinic> UpdateClinicAsync(Clinic clinic)
    {
        _appDbContext.Clinics.Update(clinic);
        await _appDbContext.SaveChangesAsync();
        return clinic;
    }

    public async Task<bool> DeleteClinicByIdAsync(int clinicId)
    {
        var clinic = await _appDbContext.Clinics.FindAsync(clinicId);
        if (clinic == null) return false;

        _appDbContext.Clinics.Remove(clinic);
        await _appDbContext.SaveChangesAsync();
        return true;
    }
}