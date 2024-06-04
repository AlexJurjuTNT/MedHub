using MedHub_Backend.Model;

namespace MedHub_Backend.Service.Interface;

public interface IUserService
{
    Task<User> CreateUserAsync(User user);
    Task<User> GetUserByIdAsync(int userId);
    Task<List<User>> GetAllUsersAsync();
    Task<User> UpdateUserAsync(User user);
    Task<bool> DeleteUserAsync(int userId);
    Task<User?> GetUserByEmail(string email);
    Task<User?> GetUserByUsername(string username);
}