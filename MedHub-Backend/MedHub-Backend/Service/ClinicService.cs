using MedHub_Backend.Data;
using MedHub_Backend.Model;
using MedHub_Backend.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace MedHub_Backend.Service;

public class ClinicService(AppDbContext appDbContext)
    : IClinicService
{
    private readonly AppDbContext _appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));


    public async Task<List<Clinic>> GetAllClinicsAsync()
    {
        return await _appDbContext.Clinics.ToListAsync();
    }

    public async Task<Clinic> GetClinicByIdAsync(int clinicId)
    {
        return await _appDbContext.Clinics.FindAsync(clinicId);
    }

    public async Task<Clinic> CreateClinicAsync(Clinic clinic)
    {
        await _appDbContext.Clinics.AddAsync(clinic);
        await _appDbContext.SaveChangesAsync();
        return clinic;
    }

    public async Task<Clinic> UpdateClinicAsync(Clinic clinic)
    {
        appDbContext.Clinics.Update(clinic);
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