using MedHub_Backend.Model;

namespace MedHub_Backend.Service.Interface;

public interface IDoctorService
{
    Task<List<User>> GetAllDoctorsAsync();
    Task<User?> GetDoctorById(int id);
    Task<bool> DeleteDoctorAsync(int id);
    Task<User> UpdateDoctorAsync(User doctor);
}