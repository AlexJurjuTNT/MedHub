using Medhub_Backend.Application.Dtos.Authentication;
using Medhub_Backend.Domain.Entities;

namespace Medhub_Backend.Application.Abstractions.Service;

public interface IAuthenticationService
{
    Task<User> RegisterPatientAsync(User user);
    Task<AuthenticationResponse> LoginUserAsync(LoginRequest loginRequest);
    Task<User> RegisterDoctorAsync(User user);
    Task<User> RegisterAdminAsync(User user);
    Task ForgotPassword(ForgotPasswordRequest forgotPasswordRequest);
    Task ResetPassword(User existingUser, string password);
}