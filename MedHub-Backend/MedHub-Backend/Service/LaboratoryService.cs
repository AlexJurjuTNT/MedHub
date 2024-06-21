using MedHub_Backend.Context;
using MedHub_Backend.Model;
using MedHub_Backend.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace MedHub_Backend.Service;

public class LaboratoryService(
    AppDbContext appDbContext
) : ILaboratoryService
{
    public async Task<List<Laboratory>> GetAllLaboratoriesAsync()
    {
        return await appDbContext.Laboratories.ToListAsync();
    }

    public async Task<Laboratory?> GetLaboratoryByIdAsync(int laboratoryId)
    {
        return await appDbContext.Laboratories.FindAsync(laboratoryId);
    }

    public async Task<Laboratory> CreateLaboratoryAsync(Laboratory laboratory, List<TestType> testTypes)
    {
        laboratory.TestTypes = testTypes;
        await appDbContext.Laboratories.AddAsync(laboratory);
        await appDbContext.SaveChangesAsync();
        return laboratory;
    }

    public async Task<bool> DeleteLaboratoryAsync(int laboratoryId)
    {
        var laboratory = await appDbContext.Laboratories.FindAsync(laboratoryId);
        if (laboratory == null) return false;

        appDbContext.Laboratories.Remove(laboratory);
        await appDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<Laboratory> UpdateLaboratoryAsync(Laboratory laboratory)
    {
        appDbContext.Laboratories.Update(laboratory);
        await appDbContext.SaveChangesAsync();
        return laboratory;
    }
}