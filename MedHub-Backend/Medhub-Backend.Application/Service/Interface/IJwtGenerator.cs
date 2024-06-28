using Medhub_Backend.Domain.Entities;

namespace Medhub_Backend.Application.Service.Interface;

public interface IJwtGenerator
{
    string GenerateToken(User user);
}