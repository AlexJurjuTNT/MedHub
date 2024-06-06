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
    // todo: send email to user
    // todo: send sms to user

    public async Task<User> RegisterPatientAsync(User user)
    {
        var userByUsername = await userService.GetUserByUsername(user.Username);
        if (userByUsername != null) throw new Exception("User already exists");

        var clinic = await clinicService.GetClinicByIdAsync(user.ClinicId);
        if (clinic == null) throw new Exception($"Clinic with it {user.ClinicId} doesnt exist");

        var patientRole = await roleService.GetRoleByName("Patient");
        if (patientRole == null) throw new Exception("Role with name Patient doesn't exist");

        user.Role = patientRole;
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        
        var createdUser = await userService.CreateUserAsync(user);
        return createdUser;
    }

    public async Task<AuthenticationResponse> LoginUserAsync(LoginRequestDto loginRequestDto)
    {
        var user = await userService.GetUserByEmail(loginRequestDto.Email);
        if (user == null) throw new Exception("User doesn't exist");

        if (!BCrypt.Net.BCrypt.Verify(loginRequestDto.Password, user.Password))
        {
            throw new Exception("Passwords don't match");
        }

        return new AuthenticationResponse()
        {
            Token = jwtService.GenerateToken(user),
            UserId = user.Id,
        };
    }

    public async Task<User> RegisterDoctorAsync(User user)
    {
        var userByUsername = await userService.GetUserByUsername(user.Username);
        if (userByUsername != null) throw new Exception("Doctor with username already exists");

        var userByEmail = await userService.GetUserByEmail(user.Email);
        if (userByEmail != null) throw new Exception("Doctor with email already exists");

        var doctorRole = await roleService.GetRoleByName("Doctor");
        if (doctorRole == null) throw new Exception("Role with name Doctor doesn't exist");

        user.Role = doctorRole;
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

        return await userService.CreateUserAsync(user);
    }
}