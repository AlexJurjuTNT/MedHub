using Medhub_Backend.Domain.Entities;

namespace Medhub_Backend.Application.Abstractions.Service;

public interface IPatientService
{
    Task<PatientInformation> CreateAsync(PatientInformation patientInformation);
    Task<PatientInformation?> GetByIdAsync(int patientId);
    Task<PatientInformation> UpdateAsync(PatientInformation patientInformation);
    Task<bool> DeleteAsync(int patientId);
}