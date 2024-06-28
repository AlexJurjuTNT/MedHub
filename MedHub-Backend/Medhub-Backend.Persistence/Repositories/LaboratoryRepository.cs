using Medhub_Backend.Application.Abstractions.Persistence;
using Medhub_Backend.Domain.Entities;
using Medhub_Backend.Persistence.Persistence;

namespace Medhub_Backend.Persistence.Repositories;

public class LaboratoryRepository : ILaboratoryRepository
{
    private readonly AppDbContext _context;

    public LaboratoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Laboratory?> GetByIdAsync(int laboratoryId)
    {
        return await _context.Laboratories.FindAsync(laboratoryId);
    }

    public async Task AddAsync(Laboratory laboratory)
    {
        await _context.Laboratories.AddAsync(laboratory);
        await _context.SaveChangesAsync();
    }

    public void Remove(Laboratory laboratory)
    {
        _context.Laboratories.Remove(laboratory);
        _context.SaveChanges();
    }

    public async Task UpdateAsync(Laboratory laboratory)
    {
        _context.Laboratories.Update(laboratory);
        await _context.SaveChangesAsync();
    }

    public IQueryable<Laboratory> GetAll()
    {
        return _context.Laboratories.AsQueryable();
    }
}