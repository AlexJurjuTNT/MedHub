using Medhub_Backend.Business.Dtos.Authentication;
using Medhub_Backend.Business.Service.Interface;
using Medhub_Backend.Domain.Exceptions;
using Medhub_Backend.Domain.Model;

namespace Medhub_Backend.Business.Service;

public class AuthenticationService(
    IUserService userService,
    IRoleService roleService,
    IClinicService clinicService,
    IJwtGenerator jwtGenerator,
    IEmailService emailService,
    IPasswordService passwordService
) : IAuthenticationService
{
    public async Task<User> RegisterPatientAsync(User user)
    {
        var userByUsername = await userService.GetUserByUsernameAsync(user.Username);
        if (userByUsername != null) throw new UserAlreadyExistsException($"User with username {user.Username} already exists");

        var clinic = await clinicService.GetClinicByIdAsync(user.ClinicId);
        if (clinic == null) throw new ClinicNotFoundException(user.ClinicId);

        user.Username = clinic.Name + clinic.Id + user.Email.Substring(0, user.Email.IndexOf('@'));
        user.Role = (await roleService.GetRoleByName("Patient"))!;
        var tempPassword = passwordService.GenerateRandomPassword(8);
        user.Password = BCrypt.Net.BCrypt.HashPassword(tempPassword);
        user.HasToResetPassword = true;

        Console.WriteLine(tempPassword);

        var createdUser = await userService.CreateUserAsync(user);
        await emailService.SendPatientResetPasswordEmail(clinic, user, tempPassword);

        return createdUser;
    }

    public async Task<AuthenticationResponse> LoginUserAsync(LoginRequest loginRequest)
    {
        var user = await userService.GetUserByUsernameAsync(loginRequest.Username);
        if (user == null) throw new UserNotFoundException($"User with username {loginRequest.Username} not found");

        if (!BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password)) throw new PasswordMismatchException();

        return new AuthenticationResponse
        {
            Token = jwtGenerator.GenerateToken(user),
            UserId = user.Id,
            HasToResetPassword = user.HasToResetPassword
        };
    }

    public async Task<User> RegisterDoctorAsync(User user)
    {
        var userByUsername = await userService.GetUserByUsernameAsync(user.Username);
        if (userByUsername != null) throw new UserAlreadyExistsException(user.Username);

        var clinic = await clinicService.GetClinicByIdAsync(user.ClinicId);
        if (clinic == null) throw new ClinicNotFoundException(user.ClinicId);

        user.Role = (await roleService.GetRoleByName("Doctor"))!;
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

        return await userService.CreateUserAsync(user);
    }

    public async Task<User> RegisterAdminAsync(User user)
    {
        var userByUsername = await userService.GetUserByUsernameAsync(user.Username);
        if (userByUsername != null) throw new UserAlreadyExistsException(user.Username);

        user.Role = (await roleService.GetRoleByName("Admin"))!;
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

        return await userService.CreateUserAsync(user);
    }

    public async Task ForgotPassword(string email)
    {
        var user = await userService.GetUserByEmail(email);
        if (user == null) throw new UserNotFoundException(email);

        user.PasswordResetCode = passwordService.GenerateRandomPassword(8);

        await emailService.SendPatientResetPasswordEmail(user.Clinic, user, user.PasswordResetCode);

        await userService.UpdateUserAsync(user);
    }

    public async Task ResetPassword(User existingUser, string password)
    {
        existingUser.PasswordResetCode = null;
        existingUser.Password = BCrypt.Net.BCrypt.HashPassword(password);
        existingUser.HasToResetPassword = false;
        await userService.UpdateUserAsync(existingUser);
    }
}