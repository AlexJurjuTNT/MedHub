using MedHub_Backend.Dto;
using MedHub_Backend.Model;
using MedHub_Backend.Service.Interface;

namespace MedHub_Backend.Service;

public class AuthenticationService(
    IUserService userService,
    IRoleService roleService,
    IClinicService clinicService,
    IJwtService jwtService,
    IPatientService patientService
) : IAuthenticationService
{
    public async Task<Patient> RegisterPatientAsync(Patient patient)
    {
        // todo: send email to patient
        // todo: send sms to patient
        // todo: better exception / replace with FluentApi Result
        var userByUsername = await userService.GetUserByUsername(patient.Username);
        if (userByUsername != null)
        {
            throw new Exception("User already exists");
        }

        var clinic = await clinicService.GetClinicByIdAsync(patient.ClinicId);
        if (clinic == null) throw new Exception($"Clinic with it {patient.ClinicId} doesnt exist");

        var patientRole = await roleService.GetRoleByName("Patient");
        if (patientRole == null) throw new Exception("Role with name Patient doesn't exist");

        patient.Role = patientRole;
        patient.PasswordHash = BCrypt.Net.BCrypt.HashPassword(patient.PasswordHash);

        return await patientService.CreatePatientAsync(patient);
    }

    public async Task<AuthenticationResponse> LoginUserAsync(LoginRequestDto loginRequestDto)
    {
        var user = await userService.GetUserByEmail(loginRequestDto.Email);
        if (user == null) throw new Exception("User doesn't exist");

        if (!BCrypt.Net.BCrypt.Verify(loginRequestDto.Password, user.PasswordHash))
        {
            throw new Exception("Passwords don't match");
        }

        return new AuthenticationResponse()
        {
            Token = jwtService.GenerateToken(user),
            UserId = user.Id,
        };
    }
}