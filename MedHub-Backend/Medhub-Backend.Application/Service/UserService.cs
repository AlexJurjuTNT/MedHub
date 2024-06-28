using Medhub_Backend.Application.Abstractions.Persistence;
using Medhub_Backend.Application.Abstractions.Service;
using Medhub_Backend.Domain.Entities;

namespace Medhub_Backend.Application.Service;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public IQueryable<User> GetAllUserPatientsAsync()
    {
        var users = _userRepository.GetAll();
        return users.Where(u => u.Role.Name == "Patient");
    }

    public async Task<User> CreateAsync(User user)
    {
        await _userRepository.AddAsync(user);
        return user;
    }

    public async Task<User?> GetByIdAsync(int userId)
    {
        return await _userRepository.GetByIdAsync(userId);
    }

    public IQueryable<User> GetAll()
    {
        return _userRepository.GetAll();
    }

    public async Task<User> UpdateAsync(User user)
    {
        await _userRepository.UpdateAsync(user);
        return user;
    }

    public async Task<bool> DeleteByIdAsync(int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null) return false;

        await _userRepository.RemoveAsync(user);
        return true;
    }

    public User? GetByEmail(string email)
    {
        var users = _userRepository.GetAll();
        return users.FirstOrDefault(u => u.Email == email);
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _userRepository.GetByUsername(username);
    }
}