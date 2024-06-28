using Medhub_Backend.Domain.Entities;

namespace Medhub_Backend.Application.Abstractions.Persistence;

public interface IPatientInformationRepository
{
    IQueryable<PatientInformation> GetAllAsync();
    Task<PatientInformation?> GetByIdAsync(int patientId);
    Task AddAsync(PatientInformation patientInformation);
    Task UpdateAsync(PatientInformation patientInformation);
    Task RemoveAsync(PatientInformation patientInformation);
}