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
    [HttpPost("login")]
    [ProducesResponseType(200, Type = typeof(AuthenticationResponse))]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
    {
        var authenticationResponse = await authenticationService.LoginUserAsync(loginRequestDto);
        return Ok(authenticationResponse);
    }

    [HttpPost("register-doctor")]
    [ProducesResponseType(200, Type = typeof(UserDto))]
    public async Task<IActionResult> RegisterDoctor([FromBody] UserRegisterDto userRegisterDto)
    {
        var user = mapper.Map<User>(userRegisterDto);
        var doctor = await authenticationService.RegisterDoctorAsync(user);
        return Ok(mapper.Map<UserDto>(doctor));
    }

    [HttpPost("register-patient")]
    [ProducesResponseType(200, Type = typeof(UserDto))]
    public async Task<IActionResult> RegisterPatient([FromBody] UserRegisterDto userDto)
    {
        var patient = mapper.Map<User>(userDto);
        var patientResult = await authenticationService.RegisterPatientAsync(patient);
        return Ok(mapper.Map<UserDto>(patientResult));
    }
}