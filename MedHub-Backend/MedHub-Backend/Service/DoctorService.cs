using MedHub_Backend.Model;
using MedHub_Backend.Service.Interface;

namespace MedHub_Backend.Service;

public class DoctorService(
    IUserService userService
) : IDoctorService
{
    public async Task<List<User>> GetAllDoctorsAsync()
    {
        var users = await userService.GetAllUsersAsync();
        return users.Where(u => u.Role.Name == "Doctor").ToList();
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