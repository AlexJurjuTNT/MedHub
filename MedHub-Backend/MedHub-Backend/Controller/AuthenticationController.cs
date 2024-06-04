using AutoMapper;
using MedHub_Backend.Dto;
using MedHub_Backend.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MedHub_Backend.Controller;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthenticationController(IAuthenticationService authenticationService, IMapper mapper) : ControllerBase
{
    [HttpPost("register-patient")]
    [ProducesResponseType(200, Type = typeof(UserDto))]
    public async Task<IActionResult> RegisterPatient([FromBody] RegisterRequestDto registerRequestDto)
    {
        var user = await authenticationService.RegisterPatientAsync(registerRequestDto);
        return Ok(mapper.Map<UserDto>(user));
    }

    [HttpPost("login")]
    [ProducesResponseType(200, Type = typeof(AuthenticationResponse))]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
    {
        var authenticationResponse = await authenticationService.LoginUserAsync(loginRequestDto);
        return Ok(authenticationResponse);
    }
}