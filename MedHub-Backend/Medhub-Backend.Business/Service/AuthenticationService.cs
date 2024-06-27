using Medhub_Backend.Business.Dtos.Authentication;
using Medhub_Backend.Business.Service.Interface;
using Medhub_Backend.Domain.Entities;
using Medhub_Backend.Domain.Exceptions;

namespace Medhub_Backend.Business.Service;

public class AuthenticationService : IAuthenticationService
{
    private readonly IClinicService _clinicService;
    private readonly IEmailService _emailService;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IPasswordService _passwordService;
    private readonly IRoleService _roleService;
    private readonly IUserService _userService;

    public AuthenticationService(IPasswordService passwordService, IEmailService emailService, IClinicService clinicService, IJwtGenerator jwtGenerator,
        IRoleService roleService, IUserService userService)
    {
        _passwordService = passwordService;
        _emailService = emailService;
        _clinicService = clinicService;
        _jwtGenerator = jwtGenerator;
        _roleService = roleService;
        _userService = userService;
    }

    public async Task<User> RegisterPatientAsync(User user)
    {
        var userByUsername = await _userService.GetUserByUsernameAsync(user.Username);
        if (userByUsername != null) throw new UserAlreadyExistsException($"User with username {user.Username} already exists");

        var clinic = await _clinicService.GetClinicByIdAsync(user.ClinicId);
        if (clinic == null) throw new ClinicNotFoundException(user.ClinicId);

        user.Username = clinic.Name + clinic.Id + user.Email.Substring(0, user.Email.IndexOf('@'));
        user.Role = (await _roleService.GetRoleByName("Patient"))!;
        var tempPassword = _passwordService.GenerateRandomPassword(8);
        user.Password = BCrypt.Net.BCrypt.HashPassword(tempPassword);
        user.HasToResetPassword = true;

        Console.WriteLine(tempPassword);

        var createdUser = await _userService.CreateUserAsync(user);
        await _emailService.SendPatientResetPasswordEmail(clinic, user, tempPassword);

        return createdUser;
    }


    public async Task<User> RegisterDoctorAsync(User user)
    {
        var userByUsername = await _userService.GetUserByUsernameAsync(user.Username);
        if (userByUsername != null) throw new UserAlreadyExistsException(user.Username);

        var clinic = await _clinicService.GetClinicByIdAsync(user.ClinicId);
        if (clinic == null) throw new ClinicNotFoundException(user.ClinicId);

        user.Role = (await _roleService.GetRoleByName("Doctor"))!;
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

        return await _userService.CreateUserAsync(user);
    }

    public async Task<User> RegisterAdminAsync(User user)
    {
        var userByUsername = await _userService.GetUserByUsernameAsync(user.Username);
        if (userByUsername != null) throw new UserAlreadyExistsException(user.Username);

        user.Role = (await _roleService.GetRoleByName("Admin"))!;
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

        return await _userService.CreateUserAsync(user);
    }

    public async Task<AuthenticationResponse> LoginUserAsync(LoginRequest loginRequest)
    {
        var user = await _userService.GetUserByUsernameAsync(loginRequest.Username);
        if (user == null) throw new UserNotFoundException($"User with username {loginRequest.Username} not found");

        if (!BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password)) throw new PasswordMismatchException();

        return new AuthenticationResponse
        {
            Token = _jwtGenerator.GenerateToken(user),
            UserId = user.Id,
            HasToResetPassword = user.HasToResetPassword
        };
    }


    public async Task ForgotPassword(ForgotPasswordRequest forgotPasswordRequest)
    {
        var user = await _userService.GetUserByUsernameAsync(forgotPasswordRequest.Username);
        if (user == null) throw new UserNotFoundException(forgotPasswordRequest.Username);

        user.PasswordResetCode = _passwordService.GenerateRandomPassword(8);

        await _emailService.SendPatientResetPasswordEmail(user.Clinic, user, user.PasswordResetCode);

        await _userService.UpdateUserAsync(user);
    }

    public async Task ResetPassword(User existingUser, string password)
    {
        existingUser.PasswordResetCode = null;
        existingUser.Password = BCrypt.Net.BCrypt.HashPassword(password);
        existingUser.HasToResetPassword = false;
        await _userService.UpdateUserAsync(existingUser);
    }
}