using MedHub_Backend.Data;
using MedHub_Backend.Model;
using MedHub_Backend.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace MedHub_Backend.Service;

public class RoleService(AppDbContext appDbContext)
    : IRoleService
{
    private readonly AppDbContext _appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));


    public async Task<Role?> GetRoleByName(string name)
    {
        return await _appDbContext.Roles.FirstOrDefaultAsync(r => r.Name == name);
    }

    public async Task<Role> AddRole(Role role)
    {
        if (role == null)
        {
            throw new ArgumentNullException(nameof(role));
        }

        var existingRole = await _appDbContext.Roles
            .Where(r => r.Name == role.Name)
            .FirstOrDefaultAsync();

        if (existingRole != null)
        {
            throw new Exception($"Role '{role.Name}' already exists.");
        }

        _appDbContext.Roles.Add(role);
        await _appDbContext.SaveChangesAsync();

        return role;
    }
}