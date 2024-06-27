using Medhub_Backend.Domain.Entities;

namespace Medhub_Backend.Business.Service.Interface;

public interface IUserService
{
    Task<User> CreateUserAsync(User user);
    Task<User?> GetUserByIdAsync(int userId);
    Task<IQueryable<User>> GetAllUsersAsync();
    Task<User> UpdateUserAsync(User user);
    Task<bool> DeleteUserAsync(int userId);
    Task<User?> GetUserByEmail(string email);
    Task<User?> GetUserByUsernameAsync(string username);
}