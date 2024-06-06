using MedHub_Backend.Context;
using MedHub_Backend.Model;
using MedHub_Backend.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace MedHub_Backend.Service;

public class UserService(
    AppDbContext appDbContext
) : IUserService
{
    public async Task<User> CreateUserAsync(User user)
    {
        await appDbContext.Users.AddAsync(user);
        await appDbContext.SaveChangesAsync();
        return user;
    }

    public async Task<User?> GetUserByIdAsync(int userId)
    {
        return await appDbContext.Users.FindAsync(userId);
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await appDbContext.Users.ToListAsync();
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        appDbContext.Users.Update(user);
        await appDbContext.SaveChangesAsync();
        return user;
    }

    public async Task<bool> DeleteUserAsync(int userId)
    {
        var user = await appDbContext.Users.FindAsync(userId);
        if (user == null) return false;

        appDbContext.Users.Remove(user);
        await appDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        var user = await appDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        return user;
    }

    public async Task<User?> GetUserByUsername(string username)
    {
        var user = await appDbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
        return user;
    }
}