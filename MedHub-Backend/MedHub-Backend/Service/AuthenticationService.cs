using MedHub_Backend.Dto;
using MedHub_Backend.Model;
using MedHub_Backend.Service.Interface;

namespace MedHub_Backend.Service;

public class AuthenticationService(
    IUserService userService,
    IRoleService roleService,
    IClinicService clinicService,
    IJwtService jwtService
) : IAuthenticationService
{
    public async Task<User> RegisterPatientAsync(RegisterRequestDto registerRequestDto)
    {
        // todo: send email to patient
        // todo: send sms to patient
        var userByUsername = await userService.GetUserByUsername(registerRequestDto.Username);
        var userByEmail = await userService.GetUserByEmail(registerRequestDto.Email);
        // todo: better exception / replace with FluentApi Result
        if (userByUsername != null || userByEmail != null)
        {
            throw new Exception("User already exists");
        }

        var clinic = await clinicService.GetClinicByIdAsync(registerRequestDto.ClinicId);
        if (clinic == null) throw new Exception("The clinic doesnt exist");

        var patientRole = await roleService.GetRoleByName("Patient");

        var user = new User()
        {
            Username = registerRequestDto.Username,
            Email = registerRequestDto.Email,
            Role = patientRole!,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerRequestDto.Password),
            Clinic = clinic
        };

        return await userService.CreateUserAsync(user);
    }

    public async Task<AuthenticationResponse> LoginUserAsync(LoginRequestDto loginRequestDto)
    {
        var user = await userService.GetUserByEmail(loginRequestDto.Email);
        if (user == null) throw new Exception("User doesn't exist");

        if (!BCrypt.Net.BCrypt.Verify(loginRequestDto.Password, user.PasswordHash))
        {
            throw new Exception("Passwords don't match");
        }

        return new AuthenticationResponse()
        {
            Token = jwtService.GenerateToken(user),
            UserId = user.Id,
        };
    }
}