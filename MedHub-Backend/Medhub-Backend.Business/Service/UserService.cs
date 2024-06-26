using Medhub_Backend.Business.Service.Interface;
using Medhub_Backend.DataAccess.Persistence;
using Medhub_Backend.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Medhub_Backend.Business.Service;

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

    public async Task<IQueryable<User>> GetAllUsersAsync()
    {
        return await Task.FromResult(appDbContext.Users);
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

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        var user = await appDbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
        return user;
    }
}