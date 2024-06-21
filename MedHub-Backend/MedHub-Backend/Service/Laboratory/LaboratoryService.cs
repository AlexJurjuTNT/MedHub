using MedHub_Backend.Context;
using Microsoft.EntityFrameworkCore;

namespace MedHub_Backend.Service.Laboratory;

public class LaboratoryService(
    AppDbContext appDbContext
) : ILaboratoryService
{
    public async Task<List<Model.Laboratory>> GetAllLaboratoriesAsync()
    {
        return await appDbContext.Laboratories.ToListAsync();
    }

    public async Task<Model.Laboratory?> GetLaboratoryByIdAsync(int laboratoryId)
    {
        return await appDbContext.Laboratories.FindAsync(laboratoryId);
    }

    public async Task<Model.Laboratory> CreateLaboratoryAsync(Model.Laboratory laboratory, List<Model.TestType> testTypes)
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

    public async Task<Model.Laboratory> UpdateLaboratoryAsync(Model.Laboratory laboratory)
    {
        appDbContext.Laboratories.Update(laboratory);
        await appDbContext.SaveChangesAsync();
        return laboratory;
    }
}