using Medhub_Backend.Business.Dtos.Authentication;
using Medhub_Backend.Domain.Model;

namespace Medhub_Backend.Business.Service.Interface;

public interface IAuthenticationService
{
    Task<User> RegisterPatientAsync(User user);
    Task<AuthenticationResponse> LoginUserAsync(LoginRequestDto loginRequestDto);
    Task<User> RegisterDoctorAsync(User user);
    Task<User> RegisterAdminAsync(User user);
    Task ForgotPassword(string email);
    Task ResetPassword(User existingUser, string password);
}