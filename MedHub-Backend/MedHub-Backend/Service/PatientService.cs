using MedHub_Backend.Data;
using MedHub_Backend.Model;
using MedHub_Backend.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace MedHub_Backend.Service;

public class PatientService(
    AppDbContext appDbContext
) : IPatientService
{
    public async Task<Patient> CreatePatientAsync(Patient patient)
    {
        Console.WriteLine(patient);
        await appDbContext.Patients.AddAsync(patient);
        await appDbContext.SaveChangesAsync();
        return patient;
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
    
    public async Task<List<Patient>> GetAllPatients()
    {
        return await appDbContext.Patients.ToListAsync();
    }

    public async Task<Patient?> GetPatientById(int patientId)
    {
        return await appDbContext.Patients.FindAsync(patientId);
    }
}