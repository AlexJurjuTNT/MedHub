using MedHub_Backend.Data;
using MedHub_Backend.Model;
using MedHub_Backend.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace MedHub_Backend.Service;

public class ClinicService(
    AppDbContext appDbContext
    ) : IClinicService
{
    
    // todo: for big lists no call to ToList -> paging in datasource 
    public async Task<List<Clinic>> GetAllClinicsAsync()
    {
        return await appDbContext.Clinics.ToListAsync();
    }

    public async Task<Clinic?> GetClinicByIdAsync(int clinicId)
    {
        return await appDbContext.Clinics.FindAsync(clinicId);
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