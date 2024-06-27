using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Medhub_Backend.Business.Dtos.Authentication;
using Medhub_Backend.Business.Dtos.User;
using Medhub_Backend.Business.Service.Interface;
using Medhub_Backend.Domain.Entities;
using Medhub_Backend.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace MedHub_Backend.WebApi.Controller;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IClinicService _clinicService;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public AuthenticationController(IAuthenticationService authenticationService, IUserService userService, IClinicService clinicService, IMapper mapper)
    {
        _authenticationService = authenticationService;
        _userService = userService;
        _clinicService = clinicService;
        _mapper = mapper;
    }

    [HttpPost("login")]
    [ProducesResponseType(200, Type = typeof(AuthenticationResponse))]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        try
        {
            var authenticationResponse = await _authenticationService.LoginUserAsync(loginRequest);
            return Ok(authenticationResponse);
        }
        catch (UserNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (PasswordMismatchException ex)
        {
            return Unauthorized(ex.Message);
        }
    }

    [HttpPost("register-admin")]
    public async Task<IActionResult> RegisterAdmin([FromBody] UserRegisterDto userRegisterDto)
    {
        var clinic = await _clinicService.GetClinicByIdAsync(userRegisterDto.ClinicId);
        if (clinic is null) return NotFound();

        try
        {
            var user = _mapper.Map<User>(userRegisterDto);
            var admin = await _authenticationService.RegisterAdminAsync(user);
            return Ok(_mapper.Map<UserDto>(admin));
        }
        catch (UserAlreadyExistsException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpPost("register-doctor")]
    public async Task<IActionResult> RegisterDoctor([FromBody] UserRegisterDto userRegisterDto)
    {
        try
        {
            var user = _mapper.Map<User>(userRegisterDto);
            var doctor = await _authenticationService.RegisterDoctorAsync(user);
            return Ok(_mapper.Map<UserDto>(doctor));
        }
        catch (UserAlreadyExistsException ex)
        {
            return Conflict(ex.Message);
        }
        catch (ClinicNotFoundException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpPost("register-patient")]
    [ProducesResponseType(200, Type = typeof(UserDto))]
    public async Task<IActionResult> RegisterPatient([FromBody] PatientRegisterDto patientRegisterDto)
    {
        try
        {
            var patient = _mapper.Map<User>(patientRegisterDto);
            var patientResult = await _authenticationService.RegisterPatientAsync(patient);
            return Ok(_mapper.Map<UserDto>(patientResult));
        }
        catch (UserAlreadyExistsException ex)
        {
            return Conflict(ex.Message);
        }
        catch (ClinicNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }


    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([Required] string email)
    {
        try
        {
            await _authenticationService.ForgotPassword(email);
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
        var existingUser = await _userService.GetUserByEmail(resetPasswordRequestDto.Email);
        if (existingUser == null) return NotFound($"User with email {resetPasswordRequestDto.Email} not found");

        if (existingUser.PasswordResetCode != resetPasswordRequestDto.PasswordResetCode) return BadRequest("Reset codes don't match");

        await _authenticationService.ResetPassword(existingUser, resetPasswordRequestDto.Password);
        return Ok();
    }

    // this is used for the patient when he first logs in and wants to change his password
    [HttpPost("change-default-password")]
    public async Task<IActionResult> ChangeDefaultPassword([FromBody] ChangeDefaultPasswordDto changeDefaultPasswordDto)
    {
        var user = await _userService.GetUserByIdAsync(changeDefaultPasswordDto.UserId);
        if (user == null) return NotFound();

        if (changeDefaultPasswordDto.Password != changeDefaultPasswordDto.ConfirmPassword) return BadRequest();
        await _authenticationService.ResetPassword(user, changeDefaultPasswordDto.Password);

        return Ok();
    }
}