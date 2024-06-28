using Medhub_Backend.Application.Abstractions.Persistence;
using Medhub_Backend.Application.Abstractions.Service;
using Medhub_Backend.Domain.Entities;

namespace Medhub_Backend.Application.Service;

public class PatientInformationService : IPatientService
{
    private readonly IPatientInformationRepository _patientInformationRepository;

    public PatientInformationService(IPatientInformationRepository patientInformationRepository)
    {
        _patientInformationRepository = patientInformationRepository;
    }

    public async Task<PatientInformation> CreateAsync(PatientInformation patientInformation)
    {
        await _patientInformationRepository.AddAsync(patientInformation);
        return patientInformation;
    }

    public async Task<PatientInformation?> GetByIdAsync(int patientId)
    {
        return await _patientInformationRepository.GetByIdAsync(patientId);
    }

    public async Task<PatientInformation> UpdateAsync(PatientInformation patientInformation)
    {
        await _patientInformationRepository.UpdateAsync(patientInformation);
        return patientInformation;
    }

    public async Task<bool> DeleteAsync(int patientId)
    {
        var patient = await _patientInformationRepository.GetByIdAsync(patientId);
        if (patient == null) return false;

        await _patientInformationRepository.RemoveAsync(patient);
        return true;
    }
}