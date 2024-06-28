using Medhub_Backend.Application.Abstractions.Service;
using Medhub_Backend.Application.Dtos.Authentication;
using Medhub_Backend.Domain.Entities;
using Medhub_Backend.Domain.Exceptions;

namespace Medhub_Backend.Application.Service;

public class AuthenticationService : IAuthenticationService
{
    private readonly IClinicService _clinicService;
    private readonly IEmailService _emailService;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IPasswordService _passwordService;
    private readonly IRoleService _roleService;
    private readonly IUserService _userService;

    public AuthenticationService(IPasswordService passwordService, IEmailService emailService, IClinicService clinicService, IJwtTokenGenerator jwtTokenGenerator,
        IRoleService roleService, IUserService userService)
    {
        _passwordService = passwordService;
        _emailService = emailService;
        _clinicService = clinicService;
        _jwtTokenGenerator = jwtTokenGenerator;
        _roleService = roleService;
        _userService = userService;
    }

    public async Task<User> RegisterPatientAsync(User user)
    {
        var userByUsername = _userService.GetByUsername(user.Username);
        if (userByUsername != null) throw new UserAlreadyExistsException(user.Username);

        var clinic = await _clinicService.GetByIdAsync(user.ClinicId);
        if (clinic == null) throw new ClinicNotFoundException(user.ClinicId);

        user.Username = clinic.Name + clinic.Id + user.Email.Substring(0, user.Email.IndexOf('@'));
        user.Role = _roleService.GetByName("Patient")!;
        var tempPassword = _passwordService.GenerateRandomPassword(8);
        user.Password = BCrypt.Net.BCrypt.HashPassword(tempPassword);
        user.HasToResetPassword = true;

        Console.WriteLine(tempPassword);

        var createdUser = await _userService.CreateAsync(user);
        await _emailService.SendPatientResetPasswordEmail(clinic, user, tempPassword);

        return createdUser;
    }


    public async Task<User> RegisterDoctorAsync(User user)
    {
        var userByUsername = _userService.GetByUsername(user.Username);
        if (userByUsername != null) throw new UserAlreadyExistsException(user.Username);

        var clinic = await _clinicService.GetByIdAsync(user.ClinicId);
        if (clinic == null) throw new ClinicNotFoundException(user.ClinicId);

        user.Role = _roleService.GetByName("Doctor")!;
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

        return await _userService.CreateAsync(user);
    }

    public async Task<User> RegisterAdminAsync(User user)
    {
        var userByUsername = _userService.GetByUsername(user.Username);
        if (userByUsername != null) throw new UserAlreadyExistsException(user.Username);

        user.Role = _roleService.GetByName("Admin")!;
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

        return await _userService.CreateAsync(user);
    }

    public AuthenticationResponse LoginUserAsync(LoginRequest loginRequest)
    {
        var user = _userService.GetByUsername(loginRequest.Username);
        if (user == null) throw new UserNotFoundException(loginRequest.Username);

        if (!BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password)) throw new PasswordMismatchException();

        return new AuthenticationResponse
        {
            Token = _jwtTokenGenerator.GenerateToken(user),
            UserId = user.Id,
            HasToResetPassword = user.HasToResetPassword
        };
    }


    public async Task ForgotPassword(ForgotPasswordRequest forgotPasswordRequest)
    {
        var user = _userService.GetByUsername(forgotPasswordRequest.Username);
        if (user == null) throw new UserNotFoundException(forgotPasswordRequest.Username);

        user.PasswordResetCode = _passwordService.GenerateRandomPassword(8);

        await _emailService.SendPatientResetPasswordEmail(user.Clinic, user, user.PasswordResetCode);

        await _userService.UpdateAsync(user);
    }

    public async Task ResetPassword(User existingUser, string password)
    {
        existingUser.PasswordResetCode = null;
        existingUser.Password = BCrypt.Net.BCrypt.HashPassword(password);
        existingUser.HasToResetPassword = false;
        await _userService.UpdateAsync(existingUser);
    }
}