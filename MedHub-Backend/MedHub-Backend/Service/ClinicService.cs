using MedHub_Backend.Context;
using MedHub_Backend.Exceptions;
using MedHub_Backend.Model;
using MedHub_Backend.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace MedHub_Backend.Service;

public class ClinicService(
    AppDbContext appDbContext
) : IClinicService
{
    public IQueryable<Clinic> GetAllClinics()
    {
        return appDbContext.Clinics;
    }

    public async Task<Clinic?> GetClinicByIdAsync(int clinicId)
    {
        return await appDbContext.Clinics.FindAsync(clinicId);
    }

    public async Task<IEnumerable<User>> GetAllPatientsOfClinicAsync(int clinicId)
    {
        var clinic = await GetClinicByIdAsync(clinicId);
        if (clinic == null) throw new ClinicNotFoundException($"Clinic with id {clinicId} not found");

        var patients = clinic.Users.Where(u => u.Role.Name == "Patient");
        return patients;
    }

    public async Task<Clinic> CreateClinicAsync(Clinic clinic)
    {
        await appDbContext.Clinics.AddAsync(clinic);
        await appDbContext.SaveChangesAsync();
        return clinic;
    }

    public async Task<Clinic> UpdateClinicAsync(Clinic clinic)
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