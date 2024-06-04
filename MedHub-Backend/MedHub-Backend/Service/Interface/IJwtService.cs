using MedHub_Backend.Model;

namespace MedHub_Backend.Service.Interface;

public interface IJwtService
{
    string GenerateToken(User user);
}