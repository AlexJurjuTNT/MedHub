using MedHub_Backend.Data;
using MedHub_Backend.Model;
using MedHub_Backend.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace MedHub_Backend.Service;

public class UserService(AppDbContext appDbContext)
    : IUserService
{
    private readonly AppDbContext _appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));

    public async Task<User> CreateUserAsync(User user)
    {
        await _appDbContext.Users.AddAsync(user);
        await _appDbContext.SaveChangesAsync();
        return user;
    }

    public async Task<User> GetUserByIdAsync(int userId)
    {
        return await _appDbContext.Users.FindAsync(userId);
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _appDbContext.Users.ToListAsync();
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        _appDbContext.Users.Update(user);
        await _appDbContext.SaveChangesAsync();
        return user;
    }

    public async Task<bool> DeleteUserAsync(int userId)
    {
        var user = await _appDbContext.Users.FindAsync(userId);
        if (user == null) return false;

        _appDbContext.Users.Remove(user);
        await _appDbContext.SaveChangesAsync();
        return true;
    }
}