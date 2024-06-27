using Medhub_Backend.Business.Service.Interface;
using Medhub_Backend.DataAccess.Persistence;
using Medhub_Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Medhub_Backend.Business.Service;

public class RoleService : IRoleService
{
    private readonly AppDbContext _appDbContext;

    public RoleService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }


    public async Task<Role?> GetRoleByName(string name)
    {
        return await _appDbContext.Roles.FirstOrDefaultAsync(r => r.Name == name);
    }

    public async Task<Role> AddRole(Role role)
    {
        if (role == null) throw new ArgumentNullException(nameof(role));

        var existingRole = await _appDbContext.Roles
            .Where(r => r.Name == role.Name)
            .FirstOrDefaultAsync();

        if (existingRole != null) throw new Exception($"Role '{role.Name}' already exists.");

        _appDbContext.Roles.Add(role);
        await _appDbContext.SaveChangesAsync();

        return role;
    }
}