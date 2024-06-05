using MedHub_Backend.Data;
using MedHub_Backend.Model;
using MedHub_Backend.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace MedHub_Backend.Service;

public class TestTypeService(
    AppDbContext appDbContext
) : ITestTypeService
{
    public async Task<List<TestType>> GetAllTestTypesAsync()
    {
        return await appDbContext.TestTypes.ToListAsync();
    }

    public async Task<TestType?> GetTestTypeByIdAsync(int testTypeId)
    {
        return await appDbContext.TestTypes.FindAsync(testTypeId);
    }

    public async Task<TestType> CreateTestTypeAsync(TestType testType)
    {
        await appDbContext.TestTypes.AddAsync(testType);
        await appDbContext.SaveChangesAsync();
        return testType;
    }
    
    public async Task<TestType> UpdateTestTypeAsync(TestType testType)
    {
        appDbContext.TestTypes.Update(testType);
        await appDbContext.SaveChangesAsync();
        return testType;
    }

    public async Task<bool> DeleteClinicByIdAsync(int testTypeId)
    {
        var testType = await appDbContext.TestTypes.FindAsync(testTypeId);
        if (testType == null) return false;
        
        appDbContext.TestTypes.Remove(testType);
        await appDbContext.SaveChangesAsync();
        return true;
    }
}