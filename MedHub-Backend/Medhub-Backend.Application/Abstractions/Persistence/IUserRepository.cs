using Medhub_Backend.Domain.Entities;

namespace Medhub_Backend.Application.Abstractions.Persistence;

public interface IUserRepository
{
    IQueryable<User> GetAll();
    Task<User?> GetByIdAsync(int userId);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task RemoveAsync(User user);
    Task<User?> GetByUsername(string username);
}