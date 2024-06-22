using Medhub_Backend.Business.Service.Interface;
using Medhub_Backend.DataAccess.Persistence;
using Medhub_Backend.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Medhub_Backend.Business.Service;

public class PatientService(
    AppDbContext appDbContext
) : IPatientService
{
    public async Task<Patient> CreatePatientAsync(Patient patient)
    {
        await appDbContext.Patients.AddAsync(patient);
        await appDbContext.SaveChangesAsync();
        return patient;
    }

    public async Task<List<User>> GetAllUserPatientsAsync()
    {
        return await appDbContext.Users.Where(u => u.Role.Name == "Patient").ToListAsync();
    }

    public async Task<Patient?> GetPatientAsync(int patientId)
    {
        return await appDbContext.Patients.FindAsync(patientId);
    }

    public async Task<Patient> UpdatePatientAsync(Patient patient)
    {
        appDbContext.Patients.Update(patient);
        await appDbContext.SaveChangesAsync();
        return patient;
    }

    public async Task<bool> DeletePatientAsync(int patientId)
    {
        var patient = await appDbContext.Patients.FindAsync(patientId);
        if (patient == null) return false;

        appDbContext.Patients.Remove(patient);
        await appDbContext.SaveChangesAsync();
        return true;
    }
}