using MedHub_Backend.Dto.Authentication;

namespace MedHub_Backend.Service.Authentication;

public interface IAuthenticationService
{
    Task<Model.User> RegisterPatientAsync(Model.User user);
    Task<AuthenticationResponse> LoginUserAsync(LoginRequestDto loginRequestDto);
    Task<Model.User> RegisterDoctorAsync(Model.User user);
    Task<Model.User> RegisterAdminAsync(Model.User user);
    Task ForgotPassword(string email);
    Task ResetPassword(Model.User existingUser, string password);
}