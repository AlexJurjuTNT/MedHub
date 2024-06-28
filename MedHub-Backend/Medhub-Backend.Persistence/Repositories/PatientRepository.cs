using Medhub_Backend.Application.Abstractions.Persistence;
using Medhub_Backend.Domain.Entities;
using Medhub_Backend.Persistence.Persistence;

namespace Medhub_Backend.Persistence.Repositories;

public class PatientRepository : IPatientRepository
{
    private readonly AppDbContext _context;

    public PatientRepository(AppDbContext context)
    {
        _context = context;
    }

    public IQueryable<Patient> GetAllAsync()
    {
        return _context.Patients.AsQueryable();
    }

    public async Task<Patient?> GetByIdAsync(int patientId)
    {
        return await _context.Patients.FindAsync(patientId);
    }

    public async Task AddAsync(Patient patient)
    {
        await _context.Patients.AddAsync(patient);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Patient patient)
    {
        _context.Patients.Update(patient);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(Patient patient)
    {
        _context.Patients.Remove(patient);
        await _context.SaveChangesAsync();
    }
}