namespace MedHub_Backend.Service.Jwt;

public interface IJwtService
{
    string GenerateToken(Model.User user);
}