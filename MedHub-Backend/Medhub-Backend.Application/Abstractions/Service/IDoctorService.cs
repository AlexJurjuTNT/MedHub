using Medhub_Backend.Domain.Entities;

namespace Medhub_Backend.Application.Abstractions.Service;

public interface IDoctorService
{
    IQueryable<User> GetAllAsync();
    Task<User?> GetByIdAsync(int id);
    Task<bool> DeleteAsync(int id);
    Task<User> UpdateAsync(User doctor);
}