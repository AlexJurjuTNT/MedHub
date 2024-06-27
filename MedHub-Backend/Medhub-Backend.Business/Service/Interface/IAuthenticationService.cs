using Medhub_Backend.Business.Dtos.Authentication;
using Medhub_Backend.Domain.Entities;

namespace Medhub_Backend.Business.Service.Interface;

public interface IAuthenticationService
{
    Task<User> RegisterPatientAsync(User user);
    Task<AuthenticationResponse> LoginUserAsync(LoginRequest loginRequest);
    Task<User> RegisterDoctorAsync(User user);
    Task<User> RegisterAdminAsync(User user);
    Task ForgotPassword(ForgotPasswordRequest forgotPasswordRequest);
    Task ResetPassword(User existingUser, string password);
}