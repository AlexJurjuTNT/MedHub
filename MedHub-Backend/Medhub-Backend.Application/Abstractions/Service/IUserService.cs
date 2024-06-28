using Medhub_Backend.Domain.Entities;

namespace Medhub_Backend.Application.Abstractions.Service;

public interface IUserService
{
    Task<User> CreateAsync(User user);
    Task<User?> GetByIdAsync(int userId);
    IQueryable<User> GetAll();
    Task<User> UpdateAsync(User user);
    Task<bool> DeleteByIdAsync(int userId);
    User? GetByEmail(string email);
    User? GetByUsername(string username);
}