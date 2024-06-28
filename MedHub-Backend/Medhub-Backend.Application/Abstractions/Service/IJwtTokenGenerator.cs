using Medhub_Backend.Domain.Entities;

namespace Medhub_Backend.Application.Abstractions.Service;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}