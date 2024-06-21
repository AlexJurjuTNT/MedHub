using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace MedHub_Backend.Service.Jwt;

public class JwtService(
    IConfiguration configuration
) : IJwtService
{
    public string GenerateToken(Model.User user)
    {
        var key = Encoding.UTF8.GetBytes(configuration["JwtSettings:SECRET_KEY"]!);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Sub, user.Username),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new("UserId", user.Id.ToString()),
            new("ClinicId", user.ClinicId.ToString()),
            new(ClaimTypes.Role, user.Role.Name!)
        };

        var tokenExpireTime = configuration["JwtSettings:TOKEN_EXPIRE_IN_HOURS"];
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(TimeSpan.FromHours(Convert.ToInt16(tokenExpireTime))),
            Issuer = configuration["JwtSettings:ISSUER"],
            Audience = configuration["JwtSettings:AUDIENCE"],
            SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var stringToken = tokenHandler.WriteToken(token);

        return stringToken;
    }
}