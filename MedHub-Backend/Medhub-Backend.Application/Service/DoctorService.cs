using Medhub_Backend.Application.Abstractions.Persistence;
using Medhub_Backend.Application.Service.Interface;
using Medhub_Backend.Domain.Entities;

namespace Medhub_Backend.Application.Service;

public class DoctorService : IDoctorService
{
    private readonly IUserRepository _userRepository;

    public DoctorService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public IQueryable<User> GetAllDoctorsAsync()
    {
        var users = _userRepository.GetAll();
        return users.Where(u => u.Role.Name == "Doctor");
    }

    public async Task<User?> GetDoctorById(int id)
    {
        var doctor = await _userRepository.GetByIdAsync(id);
        if (doctor == null || doctor.Role.Name != "Doctor") return null;
        return doctor;
    }

    public async Task<bool> DeleteDoctorAsync(int id)
    {
        var doctor = await _userRepository.GetByIdAsync(id);
        if (doctor == null || doctor.Role.Name != "Doctor") return false;

        await _userRepository.RemoveAsync(doctor);
        return true;
    }

    public async Task<User> UpdateDoctorAsync(User doctor)
    {
        await _userRepository.UpdateAsync(doctor);
        return doctor;
    }
}