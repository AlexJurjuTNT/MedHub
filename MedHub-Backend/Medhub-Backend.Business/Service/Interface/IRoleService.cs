using Medhub_Backend.Domain.Model;

namespace Medhub_Backend.Business.Service.Interface;

public interface IRoleService
{
    Task<Role?> GetRoleByName(string name);
    Task<Role> AddRole(Role role);
}