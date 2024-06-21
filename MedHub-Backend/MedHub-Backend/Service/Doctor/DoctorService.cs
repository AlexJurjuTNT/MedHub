using MedHub_Backend.Service.User;

namespace MedHub_Backend.Service.Doctor;

public class DoctorService(
    IUserService userService
) : IDoctorService
{
    public async Task<List<Model.User>> GetAllDoctorsAsync()
    {
        var users = await userService.GetAllUsersAsync();
        return users.Where(u => u.Role.Name == "Doctor").ToList();
    }

    public async Task<Model.User?> GetDoctorById(int id)
    {
        var doctor = await userService.GetUserByIdAsync(id);
        if (doctor == null || doctor.Role.Name != "Doctor") return null;
        return doctor;
    }

    public async Task<bool> DeleteDoctorAsync(int id)
    {
        return await userService.DeleteUserAsync(id);
    }

    public async Task<Model.User> UpdateDoctorAsync(Model.User doctor)
    {
        return await userService.UpdateUserAsync(doctor);
    }
}