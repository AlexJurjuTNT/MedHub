using Medhub_Backend.Domain.Entities;

namespace Medhub_Backend.Application.Abstractions.Persistence;

public interface IRoleRepository
{
    IQueryable<Role> GetAllAsync();
    Task<Role?> GetByIdAsync(int roleId);
    Task AddAsync(Role role);
    Task UpdateAsync(Role role);
    Task RemoveAsync(Role role);
}