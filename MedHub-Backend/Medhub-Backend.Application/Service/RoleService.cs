using Medhub_Backend.Application.Abstractions.Persistence;
using Medhub_Backend.Application.Abstractions.Service;
using Medhub_Backend.Domain.Entities;

namespace Medhub_Backend.Application.Service;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;

    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public Role? GetByName(string name)
    {
        var roles = _roleRepository.GetAllAsync();
        return roles.FirstOrDefault(r => r.Name == name);
    }

    public async Task<Role?> GetByIdAsync(int roleId)
    {
        var role = await _roleRepository.GetByIdAsync(roleId);
        return role;
    }

    public async Task<Role> CreateAsync(Role role)
    {
        if (role == null) throw new ArgumentNullException(nameof(role));

        var roles = _roleRepository.GetAllAsync();
        var existingRole = roles.FirstOrDefault(r => r.Name == role.Name);

        if (existingRole != null) throw new Exception($"Role '{role.Name}' already exists.");

        await _roleRepository.AddAsync(role);

        return role;
    }
}