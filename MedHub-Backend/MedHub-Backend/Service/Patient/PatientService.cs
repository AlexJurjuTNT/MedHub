using MedHub_Backend.Context;
using Microsoft.EntityFrameworkCore;

namespace MedHub_Backend.Service.Patient;

public class PatientService(
    AppDbContext appDbContext
) : IPatientService
{
    public async Task<Model.Patient> CreatePatientAsync(Model.Patient patient)
    {
        await appDbContext.Patients.AddAsync(patient);
        await appDbContext.SaveChangesAsync();
        return patient;
    }

    public async Task<List<Model.User>> GetAllUserPatientsAsync()
    {
        return await appDbContext.Users.Where(u => u.Role.Name == "Patient").ToListAsync();
    }

    public async Task<Model.Patient?> GetPatientAsync(int patientId)
    {
        return await appDbContext.Patients.FindAsync(patientId);
    }

    public async Task<Model.Patient> UpdatePatientAsync(Model.Patient patient)
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