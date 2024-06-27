using Medhub_Backend.Domain.Entities;

namespace Medhub_Backend.Business.Service.Interface;

public interface IDoctorService
{
    Task<IQueryable<User>> GetAllDoctorsAsync();
    Task<User?> GetDoctorById(int id);
    Task<bool> DeleteDoctorAsync(int id);
    Task<User> UpdateDoctorAsync(User doctor);
}