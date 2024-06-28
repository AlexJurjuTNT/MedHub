using Medhub_Backend.Domain.Entities;

namespace Medhub_Backend.Application.Service.Interface;

public interface IDoctorService
{
    IQueryable<User> GetAllDoctorsAsync();
    Task<User?> GetDoctorById(int id);
    Task<bool> DeleteDoctorAsync(int id);
    Task<User> UpdateDoctorAsync(User doctor);
}