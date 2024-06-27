using Medhub_Backend.Business.Service.Interface;
using Medhub_Backend.Domain.Entities;

namespace Medhub_Backend.Business.Service;

public class DoctorService : IDoctorService
{
    private readonly IUserService _userService;

    public DoctorService(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<IQueryable<User>> GetAllDoctorsAsync()
    {
        var users = await _userService.GetAllUsersAsync();
        return users.Where(u => u.Role.Name == "Doctor");
    }

    public async Task<User?> GetDoctorById(int id)
    {
        var doctor = await _userService.GetUserByIdAsync(id);
        if (doctor == null || doctor.Role.Name != "Doctor") return null;
        return doctor;
    }

    public async Task<bool> DeleteDoctorAsync(int id)
    {
        return await _userService.DeleteUserAsync(id);
    }

    public async Task<User> UpdateDoctorAsync(User doctor)
    {
        return await _userService.UpdateUserAsync(doctor);
    }
}