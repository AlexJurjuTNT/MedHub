using MedHub_Backend.Context;
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

    public async Task<List<TestType>> GetTestTypesFromIdList(List<int> testTypesIds)
    {
        var testTypes = await appDbContext.TestTypes
            .Where(tt => testTypesIds.Contains(tt.Id))
            .ToListAsync();

        // check if input testTypesIds contains an id that is not present in appDbContext.TestTypes
        var missingTestTypeIds = testTypesIds.Except(testTypes.Select(tt => tt.Id)).ToList();

        if (missingTestTypeIds.Any())
        {
            throw new ArgumentException($"One or more test type IDs are not valid: {string.Join(", ", missingTestTypeIds)}");
        }

        return testTypes;
    }
}