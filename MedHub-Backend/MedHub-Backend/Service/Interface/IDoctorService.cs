using MedHub_Backend.Model;

namespace MedHub_Backend.Service.Interface;

public interface IDoctorService
{
    Task<List<User>> GetAllDoctorsAsync();
    Task<User?> GetDoctorByIdAsync(int id);
    Task<bool> DeleteDoctor(int id);
    Task<User> UpdateDoctor(User doctor);
}