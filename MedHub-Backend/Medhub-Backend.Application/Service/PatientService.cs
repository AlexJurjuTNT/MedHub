using Medhub_Backend.Application.Abstractions.Persistence;
using Medhub_Backend.Application.Service.Interface;
using Medhub_Backend.Domain.Entities;

namespace Medhub_Backend.Application.Service;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;
    private readonly IUserRepository _userRepository;

    public PatientService(IUserRepository userRepository, IPatientRepository patientRepository)
    {
        _userRepository = userRepository;
        _patientRepository = patientRepository;
    }

    public async Task<Patient> CreatePatientAsync(Patient patient)
    {
        await _patientRepository.AddAsync(patient);
        return patient;
    }

    public IQueryable<User> GetAllUserPatientsAsync()
    {
        var users = _userRepository.GetAll();
        return users.Where(u => u.Role.Name == "Patient");
    }

    public async Task<Patient?> GetPatientAsync(int patientId)
    {
        return await _patientRepository.GetByIdAsync(patientId);
    }

    public async Task<Patient> UpdatePatientAsync(Patient patient)
    {
        await _patientRepository.UpdateAsync(patient);
        return patient;
    }

    public async Task<bool> DeletePatientAsync(int patientId)
    {
        var patient = await _patientRepository.GetByIdAsync(patientId);
        if (patient == null) return false;

        await _patientRepository.RemoveAsync(patient);
        return true;
    }
}