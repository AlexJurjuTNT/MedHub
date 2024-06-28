using Medhub_Backend.Domain.Entities;

namespace Medhub_Backend.Application.Service.Interface;

public interface IUserService
{
    Task<User> CreateUserAsync(User user);
    Task<User?> GetUserByIdAsync(int userId);
    IQueryable<User> GetAllUsers();
    Task<User> UpdateUserAsync(User user);
    Task<bool> DeleteUserAsync(int userId);
    User? GetUserByEmail(string email);
    User? GetUserByUsername(string username);
}