using Medhub_Backend.Business.Service.Interface;
using Medhub_Backend.DataAccess.Persistence;
using Medhub_Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Medhub_Backend.Business.Service;

public class TestTypeService : ITestTypeService
{
    private readonly AppDbContext _appDbContext;

    public TestTypeService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }


    public IQueryable<TestType> GetAllTestTypes()
    {
        return _appDbContext.TestTypes;
    }

    public async Task<TestType?> GetTestTypeByIdAsync(int testTypeId)
    {
        return await _appDbContext.TestTypes.FindAsync(testTypeId);
    }

    public async Task<TestType> CreateTestTypeAsync(TestType testType)
    {
        await _appDbContext.TestTypes.AddAsync(testType);
        await _appDbContext.SaveChangesAsync();
        return testType;
    }

    public async Task<TestType> UpdateTestTypeAsync(TestType testType)
    {
        _appDbContext.TestTypes.Update(testType);
        await _appDbContext.SaveChangesAsync();
        return testType;
    }

    public async Task<bool> DeleteClinicByIdAsync(int testTypeId)
    {
        var testType = await _appDbContext.TestTypes.FindAsync(testTypeId);
        if (testType == null) return false;

        _appDbContext.TestTypes.Remove(testType);
        await _appDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<List<TestType>> GetTestTypesFromIdList(List<int> testTypesIds)
    {
        var testTypes = await _appDbContext.TestTypes
            .Where(tt => testTypesIds.Contains(tt.Id))
            .ToListAsync();

        // check if input testTypesIds contains an id that is not present in appDbContext.TestTypes
        var missingTestTypeIds = testTypesIds.Except(testTypes.Select(tt => tt.Id)).ToList();

        if (missingTestTypeIds.Any()) throw new ArgumentException($"One or more test type IDs are not valid: {string.Join(", ", missingTestTypeIds)}");

        return testTypes;
    }
}