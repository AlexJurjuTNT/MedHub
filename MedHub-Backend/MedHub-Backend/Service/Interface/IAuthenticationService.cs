using MedHub_Backend.Dto;
using MedHub_Backend.Model;

namespace MedHub_Backend.Service.Interface;

public interface IAuthenticationService
{
    Task<User> RegisterPatientAsync(User user);
    Task<AuthenticationResponse> LoginUserAsync(LoginRequestDto loginRequestDto);
    Task<User> RegisterDoctorAsync(User user);
    Task ForgotPassword(string email);
    Task ResetPassword(User existingUser, string password);
}