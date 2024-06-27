using Medhub_Backend.Business.Service.Interface;
using Medhub_Backend.DataAccess.Persistence;
using Medhub_Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Medhub_Backend.Business.Service;

public class LaboratoryService : ILaboratoryService
{
    private readonly AppDbContext _appDbContext;

    public LaboratoryService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }


    public async Task<List<Laboratory>> GetAllLaboratoriesAsync()
    {
        return await _appDbContext.Laboratories.ToListAsync();
    }

    public async Task<Laboratory?> GetLaboratoryByIdAsync(int laboratoryId)
    {
        return await _appDbContext.Laboratories.FindAsync(laboratoryId);
    }

    public async Task<Laboratory> CreateLaboratoryAsync(Laboratory laboratory, List<TestType> testTypes)
    {
        laboratory.TestTypes = testTypes;
        await _appDbContext.Laboratories.AddAsync(laboratory);
        await _appDbContext.SaveChangesAsync();
        return laboratory;
    }

    public async Task<bool> DeleteLaboratoryAsync(int laboratoryId)
    {
        var laboratory = await _appDbContext.Laboratories.FindAsync(laboratoryId);
        if (laboratory == null) return false;

        _appDbContext.Laboratories.Remove(laboratory);
        await _appDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<Laboratory> UpdateLaboratoryAsync(Laboratory laboratory)
    {
        _appDbContext.Laboratories.Update(laboratory);
        await _appDbContext.SaveChangesAsync();
        return laboratory;
    }
}