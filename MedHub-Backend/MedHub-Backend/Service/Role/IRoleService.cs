using MedHub_Backend.Model;

namespace MedHub_Backend.Service;

public interface IRoleService
{
    Task<Role?> GetRoleByName(string name);
    Task<Role> AddRole(Role role);
}