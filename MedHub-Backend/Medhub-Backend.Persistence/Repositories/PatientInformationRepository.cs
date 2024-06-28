using Medhub_Backend.Application.Abstractions.Persistence;
using Medhub_Backend.Domain.Entities;
using Medhub_Backend.Persistence.Persistence;

namespace Medhub_Backend.Persistence.Repositories;

public class PatientInformationRepository : IPatientInformationRepository
{
    private readonly AppDbContext _context;

    public PatientInformationRepository(AppDbContext context)
    {
        _context = context;
    }

    public IQueryable<PatientInformation> GetAllAsync()
    {
        return _context.Patients.AsQueryable();
    }

    public async Task<PatientInformation?> GetByIdAsync(int patientId)
    {
        return await _context.Patients.FindAsync(patientId);
    }

    public async Task AddAsync(PatientInformation patientInformation)
    {
        await _context.Patients.AddAsync(patientInformation);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(PatientInformation patientInformation)
    {
        _context.Patients.Update(patientInformation);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(PatientInformation patientInformation)
    {
        _context.Patients.Remove(patientInformation);
        await _context.SaveChangesAsync();
    }
}