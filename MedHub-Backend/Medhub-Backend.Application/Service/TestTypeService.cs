using Medhub_Backend.Application.Abstractions.Persistence;
using Medhub_Backend.Application.Service.Interface;
using Medhub_Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Medhub_Backend.Application.Service;

public class TestTypeService : ITestTypeService
{
    private readonly ITestTypeRepository _testTypeRepository;

    public TestTypeService(ITestTypeRepository testTypeRepository)
    {
        _testTypeRepository = testTypeRepository;
    }

    public IQueryable<TestType> GetAllTestTypesAsync()
    {
        return _testTypeRepository.GetAllAsync();
    }

    public async Task<TestType?> GetTestTypeByIdAsync(int testTypeId)
    {
        return await _testTypeRepository.GetByIdAsync(testTypeId);
    }

    public async Task<TestType> CreateTestTypeAsync(TestType testType)
    {
        await _testTypeRepository.AddAsync(testType);
        return testType;
    }

    public async Task<TestType> UpdateTestTypeAsync(TestType testType)
    {
        await _testTypeRepository.UpdateAsync(testType);
        return testType;
    }

    public async Task<bool> DeleteTestTypeByIdAsync(int testTypeId)
    {
        var testType = await _testTypeRepository.GetByIdAsync(testTypeId);
        if (testType == null) return false;

        await _testTypeRepository.RemoveAsync(testType);
        return true;
    }

    public async Task<List<TestType>> GetTestTypesFromIdList(List<int> testTypesIds)
    {
        var allTestTypes = _testTypeRepository.GetAllAsync();
        var testTypes = await allTestTypes.Where(tt => testTypesIds.Contains(tt.Id)).ToListAsync();

        var missingTestTypeIds = testTypesIds.Except(testTypes.Select(tt => tt.Id)).ToList();

        if (missingTestTypeIds.Any())
            throw new ArgumentException($"One or more test type IDs are not valid: {string.Join(", ", missingTestTypeIds)}");

        return testTypes;
    }
}