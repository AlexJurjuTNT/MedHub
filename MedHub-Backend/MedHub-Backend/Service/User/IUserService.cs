namespace MedHub_Backend.Service.User;

public interface IUserService
{
    Task<Model.User> CreateUserAsync(Model.User user);
    Task<Model.User?> GetUserByIdAsync(int userId);
    Task<List<Model.User>> GetAllUsersAsync();
    Task<Model.User> UpdateUserAsync(Model.User user);
    Task<bool> DeleteUserAsync(int userId);
    Task<Model.User?> GetUserByEmail(string email);
    Task<Model.User?> GetUserByUsernameAsync(string username);
}