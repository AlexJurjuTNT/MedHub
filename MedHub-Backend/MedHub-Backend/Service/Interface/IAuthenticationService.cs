using MedHub_Backend.Dto;
using MedHub_Backend.Model;

namespace MedHub_Backend.Service.Interface;

public interface IAuthenticationService
{
    Task<Patient> RegisterPatientAsync(Patient patient);
    Task<AuthenticationResponse> LoginUserAsync(LoginRequestDto loginRequestDto);
}