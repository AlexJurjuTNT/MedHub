using Medhub_Backend.Domain.Entities;

namespace Medhub_Backend.Business.Service.Interface;

public interface IJwtGenerator
{
    string GenerateToken(User user);
}