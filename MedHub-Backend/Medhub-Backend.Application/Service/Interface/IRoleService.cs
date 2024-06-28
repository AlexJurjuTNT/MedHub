using Medhub_Backend.Domain.Entities;

namespace Medhub_Backend.Application.Service.Interface;

public interface IRoleService
{
    Role? GetRoleByName(string name);
    Task<Role> AddRole(Role role);
}