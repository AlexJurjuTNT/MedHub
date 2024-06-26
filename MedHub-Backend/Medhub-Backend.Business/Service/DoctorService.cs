using Medhub_Backend.Business.Service.Interface;
using Medhub_Backend.Domain.Model;

namespace Medhub_Backend.Business.Service;

public class DoctorService(
    IUserService userService
) : IDoctorService
{
    public async Task<IQueryable<User>> GetAllDoctorsAsync()
    {
        var users = await userService.GetAllUsersAsync();
        return users.Where(u => u.Role.Name == "Doctor");
    }

    public async Task<User?> GetDoctorById(int id)
    {
        var doctor = await userService.GetUserByIdAsync(id);
        if (doctor == null || doctor.Role.Name != "Doctor") return null;
        return doctor;
    }

    public async Task<bool> DeleteDoctorAsync(int id)
    {
        return await userService.DeleteUserAsync(id);
    }

    public async Task<User> UpdateDoctorAsync(User doctor)
    {
        return await userService.UpdateUserAsync(doctor);
    }
}