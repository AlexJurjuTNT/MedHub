using AutoMapper;
using MedHub_Backend.Dto;
using MedHub_Backend.Model;
using MedHub_Backend.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MedHub_Backend.Controller;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthenticationController(IAuthenticationService authenticationService, IMapper mapper) : ControllerBase
{
    [HttpPost("register-patient")]
    [ProducesResponseType(200, Type = typeof(PatientDto))]
    public async Task<IActionResult> RegisterPatient([FromBody] RegisterPatientDto registerPatientDto)
    {
        var patient = mapper.Map<Patient>(registerPatientDto);
        var patientResult = await authenticationService.RegisterPatientAsync(patient);
        return Ok(mapper.Map<PatientDto>(patientResult));
    }

    [HttpPost("login")]
    [ProducesResponseType(200, Type = typeof(AuthenticationResponse))]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
    {
        var authenticationResponse = await authenticationService.LoginUserAsync(loginRequestDto);
        return Ok(authenticationResponse);
    }
}