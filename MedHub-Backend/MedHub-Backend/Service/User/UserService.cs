using MedHub_Backend.Context;
using Microsoft.EntityFrameworkCore;

namespace MedHub_Backend.Service.User;

public class UserService(
    AppDbContext appDbContext
) : IUserService
{
    public async Task<Model.User> CreateUserAsync(Model.User user)
    {
        await appDbContext.Users.AddAsync(user);
        await appDbContext.SaveChangesAsync();
        return user;
    }

    public async Task<Model.User?> GetUserByIdAsync(int userId)
    {
        return await appDbContext.Users.FindAsync(userId);
    }

    public async Task<List<Model.User>> GetAllUsersAsync()
    {
        return await appDbContext.Users.ToListAsync();
    }

    public async Task<Model.User> UpdateUserAsync(Model.User user)
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

    public async Task<Model.User?> GetUserByEmail(string email)
    {
        var user = await appDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        return user;
    }

    public async Task<Model.User?> GetUserByUsernameAsync(string username)
    {
        var user = await appDbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
        return user;
    }
}