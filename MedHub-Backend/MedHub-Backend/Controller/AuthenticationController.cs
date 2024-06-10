using System.ComponentModel.DataAnnotations;
using AutoMapper;
using MedHub_Backend.Dto;
using MedHub_Backend.Exceptions;
using MedHub_Backend.Model;
using MedHub_Backend.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedHub_Backend.Controller;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthenticationController(
    IAuthenticationService authenticationService,
    IUserService userService,
    IMapper mapper
) : ControllerBase
{
    [HttpPost("login")]
    [ProducesResponseType(200, Type = typeof(AuthenticationResponse))]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
    {
        try
        {
            var authenticationResponse = await authenticationService.LoginUserAsync(loginRequestDto);
            return Ok(authenticationResponse);
        }
        catch (UserNotFoundException ex)
        {
            return NotFound(ex.Message);
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
    [ProducesResponseType(200, Type = typeof(UserDto))]
    public async Task<IActionResult> RegisterPatient([FromBody] PatientRegisterDto patientRegisterDto)
    {
        try
        {
            var patient = mapper.Map<User>(patientRegisterDto);
            var patientResult = await authenticationService.RegisterPatientAsync(patient);
            return Ok(mapper.Map<UserDto>(patientResult));
        }
        catch (UserAlreadyExistsException)
        {
            return Conflict("User already exists");
        }
        catch (ClinicNotFoundException)
        {
            return NotFound($"Clinic with id {patientRegisterDto.ClinicId} doesn't exist");
        }
        catch (RoleNotFoundException)
        {
            return BadRequest("Role with name Patient doesn't exist");
        }
    }


    [AllowAnonymous]
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([Required] string email)
    {
        try
        {
            await authenticationService.ForgotPassword(email);
        }
        catch (UserNotFoundException ex)
        {
            return NotFound(ex.Message);
        }

        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto resetPasswordRequestDto)
    {
        var existingUser = await userService.GetUserByEmail(resetPasswordRequestDto.EmailAddress);
        if (existingUser == null)
        {
            return NotFound($"User with email {resetPasswordRequestDto.EmailAddress} not found");
        }

        if (existingUser.PasswordResetCode != resetPasswordRequestDto.PasswordResetCode)
        {
            return BadRequest($"Reset codes don't match");
        }

        await authenticationService.ResetPassword(existingUser, resetPasswordRequestDto.Password);
        return Ok();
    }
}