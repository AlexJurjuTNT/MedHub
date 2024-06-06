using MedHub_Backend.Dto;
using MedHub_Backend.Exceptions;
using MedHub_Backend.Model;
using MedHub_Backend.Service.Interface;

namespace MedHub_Backend.Service;

public class AuthenticationService(
    IUserService userService,
    IRoleService roleService,
    IClinicService clinicService,
    IJwtService jwtService,
    IEmailService emailService,
    IPasswordService passwordService
) : IAuthenticationService
{
    // todo: send sms to user
    // todo: RegisterPatient should give the patient a random username / a username based on the clinic
    
    public async Task<User> RegisterPatientAsync(User user)
    {
        var userByUsername = await userService.GetUserByUsername(user.Username);
        if (userByUsername != null) throw new UserAlreadyExistsException("User already exists");

        var clinic = await clinicService.GetClinicByIdAsync(user.ClinicId);
        if (clinic == null) throw new ClinicNotFoundException($"Clinic with id {user.ClinicId} doesn't exist");

        var patientRole = await roleService.GetRoleByName("Patient");
        if (patientRole == null) throw new RoleNotFoundException("Role with name Patient doesn't exist");

        var tempPassword = passwordService.GenerateRandomPassword(8);
        user.Role = patientRole;
        user.Password = BCrypt.Net.BCrypt.HashPassword(tempPassword);
        
        var createdUser = await userService.CreateUserAsync(user);
        await emailService.SendPatientResetEmail(user.Email, user.Username, tempPassword);

        return createdUser;
    }

    public async Task<AuthenticationResponse> LoginUserAsync(LoginRequestDto loginRequestDto)
    {
        var user = await userService.GetUserByEmail(loginRequestDto.Email);
        if (user == null) throw new UserNotFoundException("User doesn't exist");

        if (!BCrypt.Net.BCrypt.Verify(loginRequestDto.Password, user.Password)) throw new PasswordMismatchException("Passwords don't match");

        return new AuthenticationResponse()
        {
            Token = jwtService.GenerateToken(user),
            UserId = user.Id
        };
    }

    public async Task<User> RegisterDoctorAsync(User user)
    {
        var userByUsername = await userService.GetUserByUsername(user.Username);
        if (userByUsername != null) throw new UserAlreadyExistsException("Doctor with username already exists");

        var userByEmail = await userService.GetUserByEmail(user.Email);
        if (userByEmail != null) throw new UserAlreadyExistsException("Doctor with email already exists");

        var doctorRole = await roleService.GetRoleByName("Doctor");
        if (doctorRole == null) throw new RoleNotFoundException("Role with name Doctor doesn't exist");

        user.Role = doctorRole;
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

        return await userService.CreateUserAsync(user);
    }
}