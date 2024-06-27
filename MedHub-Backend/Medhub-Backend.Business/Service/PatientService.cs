using Medhub_Backend.Business.Service.Interface;
using Medhub_Backend.DataAccess.Persistence;
using Medhub_Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Medhub_Backend.Business.Service;

public class PatientService : IPatientService
{
    private readonly AppDbContext _appDbContext;

    public PatientService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }


    public async Task<Patient> CreatePatientAsync(Patient patient)
    {
        await _appDbContext.Patients.AddAsync(patient);
        await _appDbContext.SaveChangesAsync();
        return patient;
    }

    public async Task<List<User>> GetAllUserPatientsAsync()
    {
        return await _appDbContext.Users.Where(u => u.Role.Name == "Patient").ToListAsync();
    }

    public async Task<Patient?> GetPatientAsync(int patientId)
    {
        return await _appDbContext.Patients.FindAsync(patientId);
    }

    public async Task<Patient> UpdatePatientAsync(Patient patient)
    {
        _appDbContext.Patients.Update(patient);
        await _appDbContext.SaveChangesAsync();
        return patient;
    }

    public async Task<bool> DeletePatientAsync(int patientId)
    {
        var patient = await _appDbContext.Patients.FindAsync(patientId);
        if (patient == null) return false;

        _appDbContext.Patients.Remove(patient);
        await _appDbContext.SaveChangesAsync();
        return true;
    }
}