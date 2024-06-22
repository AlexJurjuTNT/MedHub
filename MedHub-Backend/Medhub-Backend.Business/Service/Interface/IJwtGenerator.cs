using Medhub_Backend.Domain.Model;

namespace Medhub_Backend.Business.Service.Interface;

public interface IJwtGenerator
{
    string GenerateToken(User user);
}