using Medhub_Backend.Application.Abstractions.Persistence;
using Medhub_Backend.Application.Service.Interface;
using Medhub_Backend.Domain.Entities;

namespace Medhub_Backend.Application.Service;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> CreateUserAsync(User user)
    {
        await _userRepository.AddAsync(user);
        return user;
    }

    public async Task<User?> GetUserByIdAsync(int userId)
    {
        return await _userRepository.GetByIdAsync(userId);
    }

    public IQueryable<User> GetAllUsers()
    {
        return _userRepository.GetAll();
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        await _userRepository.UpdateAsync(user);
        return user;
    }

    public async Task<bool> DeleteUserAsync(int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null) return false;

        await _userRepository.RemoveAsync(user);
        return true;
    }

    public User? GetUserByEmail(string email)
    {
        var users = _userRepository.GetAll();
        return users.FirstOrDefault(u => u.Email == email);
    }

    public User? GetUserByUsername(string username)
    {
        var users = _userRepository.GetAll();
        return users.FirstOrDefault(u => u.Username == username);
    }
}