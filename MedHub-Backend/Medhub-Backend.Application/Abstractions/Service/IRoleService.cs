using Medhub_Backend.Domain.Entities;

namespace Medhub_Backend.Application.Abstractions.Service;

public interface IRoleService
{
    Role? GetByName(string name);
    Task<Role?> GetByIdAsync(int roleId);
    Task<Role> CreateAsync(Role role);
}