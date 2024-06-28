using Medhub_Backend.Application.Abstractions.Persistence;
using Medhub_Backend.Application.Abstractions.Service;
using Medhub_Backend.Domain.Entities;

namespace Medhub_Backend.Application.Service;

public class LaboratoryService : ILaboratoryService
{
    private readonly ILaboratoryRepository _laboratoryRepository;

    public LaboratoryService(ILaboratoryRepository laboratoryRepository)
    {
        _laboratoryRepository = laboratoryRepository;
    }

    public IQueryable<Laboratory> GetAllAsync()
    {
        return _laboratoryRepository.GetAll();
    }

    public async Task<Laboratory?> GetByIdAsync(int laboratoryId)
    {
        return await _laboratoryRepository.GetByIdAsync(laboratoryId);
    }

    public async Task<Laboratory> CreateAsync(Laboratory laboratory, List<TestType> testTypes)
    {
        laboratory.TestTypes = testTypes;
        await _laboratoryRepository.AddAsync(laboratory);
        return laboratory;
    }

    public async Task<bool> DeleteAsync(int laboratoryId)
    {
        var laboratory = await _laboratoryRepository.GetByIdAsync(laboratoryId);
        if (laboratory == null) return false;

        _laboratoryRepository.Remove(laboratory);
        return true;
    }

    public async Task<Laboratory> UpdateAsync(Laboratory laboratory)
    {
        await _laboratoryRepository.UpdateAsync(laboratory);
        return laboratory;
    }
}