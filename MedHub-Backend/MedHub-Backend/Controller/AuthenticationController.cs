using System.ComponentModel.DataAnnotations;
using AutoMapper;
using MedHub_Backend.Dto.Authentication;
using MedHub_Backend.Dto.User;
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

    [HttpPost("register-admin")]
    public async Task<IActionResult> RegisterAdmin([FromBody] UserRegisterDto userRegisterDto)
    {
        try
        {
            var user = mapper.Map<User>(userRegisterDto);
            var admin = await authenticationService.RegisterAdminAsync(user);
            return Ok(mapper.Map<UserDto>(admin));
        }
        catch (UserAlreadyExistsException)
        {
            return Conflict("Admin with username or email already exists");
        }
        catch (RoleNotFoundException)
        {
            return BadRequest("Admin with name Doctor doesn't exist");
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

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto resetPasswordRequestDto)
    {
        var existingUser = await userService.GetUserByEmail(resetPasswordRequestDto.Email);
        if (existingUser == null) return NotFound($"User with email {resetPasswordRequestDto.Email} not found");

        if (existingUser.PasswordResetCode != resetPasswordRequestDto.PasswordResetCode) return BadRequest("Reset codes don't match");

        await authenticationService.ResetPassword(existingUser, resetPasswordRequestDto.Password);
        return Ok();
    }

    // this is used for the patient when he first logs in and wants to change his password
    [HttpPost("change-default-password")]
    public async Task<IActionResult> ChangeDefaultPassword([FromBody] ChangeDefaultPasswordDto changeDefaultPasswordDto)
    {
        var user = await userService.GetUserByIdAsync(changeDefaultPasswordDto.UserId);
        if (user == null) return NotFound();

        if (changeDefaultPasswordDto.Password != changeDefaultPasswordDto.ConfirmPassword) return BadRequest();
        await authenticationService.ResetPassword(user, changeDefaultPasswordDto.Password);

        
        
        return Ok();
    }
}