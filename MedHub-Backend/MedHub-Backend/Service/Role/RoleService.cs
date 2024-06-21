using MedHub_Backend.Context;
using MedHub_Backend.Model;
using Microsoft.EntityFrameworkCore;

namespace MedHub_Backend.Service;

public class RoleService(
    AppDbContext appDbContext
) : IRoleService
{
    public async Task<Role?> GetRoleByName(string name)
    {
        return await appDbContext.Roles.FirstOrDefaultAsync(r => r.Name == name);
    }

    public async Task<Role> AddRole(Role role)
    {
        if (role == null) throw new ArgumentNullException(nameof(role));

        var existingRole = await appDbContext.Roles
            .Where(r => r.Name == role.Name)
            .FirstOrDefaultAsync();

        if (existingRole != null) throw new Exception($"Role '{role.Name}' already exists.");

        appDbContext.Roles.Add(role);
        await appDbContext.SaveChangesAsync();

        return role;
    }
}