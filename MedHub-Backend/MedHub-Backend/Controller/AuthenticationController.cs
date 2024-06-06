using AutoMapper;
using MedHub_Backend.Dto;
using MedHub_Backend.Exceptions;
using MedHub_Backend.Model;
using MedHub_Backend.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MedHub_Backend.Controller;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthenticationController(IAuthenticationService authenticationService, IMapper mapper) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
    {
        try
        {
            var authenticationResponse = await authenticationService.LoginUserAsync(loginRequestDto);
            return Ok(authenticationResponse);
        }
        catch (UserNotFoundException)
        {
            return NotFound("User doesn't exist");
        }
        catch (PasswordMismatchException)
        {
            return Unauthorized("Passwords don't match");
        }
    }

    [HttpPost("register-doctor")]
    public async Task<IActionResult> RegisterDoctor([FromBody] UserRegisterDto userRegisterDto)
    {
        try
        {
            var user = mapper.Map<User>(userRegisterDto);
            var doctor = await authenticationService.RegisterDoctorAsync(user);
            return Ok(mapper.Map<UserDto>(doctor));
        }
        catch (UserAlreadyExistsException)
        {
            return Conflict("Doctor with username or email already exists");
        }
        catch (RoleNotFoundException)
        {
            return BadRequest("Role with name Doctor doesn't exist");
        }
    }

    [HttpPost("register-patient")]
    public async Task<IActionResult> RegisterPatient([FromBody] UserRegisterDto userDto)
    {
        try
        {
            var patient = mapper.Map<User>(userDto);
            var patientResult = await authenticationService.RegisterPatientAsync(patient);
            return Ok(mapper.Map<UserDto>(patientResult));
        }
        catch (UserAlreadyExistsException)
        {
            return Conflict("User already exists");
        }
        catch (ClinicNotFoundException)
        {
            return NotFound($"Clinic with id {userDto.ClinicId} doesn't exist");
        }
        catch (RoleNotFoundException)
        {
            return BadRequest("Role with name Patient doesn't exist");
        }
    }
}