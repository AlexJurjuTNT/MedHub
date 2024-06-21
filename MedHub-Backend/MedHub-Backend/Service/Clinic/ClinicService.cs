using MedHub_Backend.Context;
using MedHub_Backend.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace MedHub_Backend.Service.Clinic;

public class ClinicService(
    AppDbContext appDbContext
) : IClinicService
{
    public async Task<List<Model.Clinic>> GetAllClinicsAsync()
    {
        return await appDbContext.Clinics.ToListAsync();
    }

    public async Task<Model.Clinic?> GetClinicByIdAsync(int clinicId)
    {
        return await appDbContext.Clinics.FindAsync(clinicId);
    }

    public async Task<IEnumerable<Model.User>> GetAllPatientsOfClinicAsync(int clinicId)
    {
        var clinic = await GetClinicByIdAsync(clinicId);
        if (clinic == null) throw new ClinicNotFoundException($"Clinic with id {clinicId} not found");

        var patients = clinic.Users.Where(u => u.Role.Name == "Patient");
        return patients;
    }

    public async Task<Model.Clinic> CreateClinicAsync(Model.Clinic clinic)
    {
        await appDbContext.Clinics.AddAsync(clinic);
        await appDbContext.SaveChangesAsync();
        return clinic;
    }

    public async Task<Model.Clinic> UpdateClinicAsync(Model.Clinic clinic)
    {
        appDbContext.Clinics.Update(clinic);
        await appDbContext.SaveChangesAsync();
        return clinic;
    }

    public async Task<bool> DeleteClinicByIdAsync(int clinicId)
    {
        var clinic = await appDbContext.Clinics.FindAsync(clinicId);
        if (clinic == null) return false;

        appDbContext.Clinics.Remove(clinic);
        await appDbContext.SaveChangesAsync();
        return true;
    }
}