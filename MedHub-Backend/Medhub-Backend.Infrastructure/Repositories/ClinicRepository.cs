using Medhub_Backend.Application.Abstractions.Persistence;
using Medhub_Backend.Domain.Entities;
using Medhub_Backend.Infrastructure.Persistence;

namespace Medhub_Backend.Infrastructure.Repositories;

public class ClinicRepository : IClinicRepository
{
    private readonly AppDbContext _context;

    public ClinicRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task UpdateAsync(Clinic clinic)
    {
        _context.Clinics.Update(clinic);
        await _context.SaveChangesAsync();
    }

    public async Task AddAsync(Clinic clinic)
    {
        await _context.Clinics.AddAsync(clinic);
        await _context.SaveChangesAsync();
    }

    public async Task<Clinic?> GetByIdAsync(int clinicId)
    {
        return await _context.Clinics.FindAsync(clinicId);
    }

    public IQueryable<Clinic> GetAll()
    {
        return _context.Clinics;
    }

    public void Remove(Clinic clinic)
    {
        _context.Clinics.Remove(clinic);
        _context.SaveChanges();
    }
}