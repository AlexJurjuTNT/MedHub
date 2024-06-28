using AutoMapper;
using Medhub_Backend.Application.Abstractions.Service;
using Medhub_Backend.Application.Dtos.Authentication;
using Medhub_Backend.Application.Dtos.User;
using Medhub_Backend.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedHub_Backend.WebApi.Controller;

[AllowAnonymous]
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
        AuthenticationResponse authenticationResponse = await _authenticationService.LoginUserAsync(loginRequest);
        return Ok(authenticationResponse);
    }

    [HttpPost("register-admin")]
    public async Task<IActionResult> RegisterAdmin([FromBody] UserRegisterRequest userRegisterRequest)
    {
        var clinic = await _clinicService.GetByIdAsync(userRegisterRequest.ClinicId);
        if (clinic is null) return NotFound();

        var user = _mapper.Map<User>(userRegisterRequest);
        var admin = await _authenticationService.RegisterAdminAsync(user);
        return Ok(_mapper.Map<UserDto>(admin));
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("register-doctor")]
    public async Task<IActionResult> RegisterDoctor([FromBody] UserRegisterRequest userRegisterRequest)
    {
        var user = _mapper.Map<User>(userRegisterRequest);
        var doctor = await _authenticationService.RegisterDoctorAsync(user);
        return Ok(_mapper.Map<UserDto>(doctor));
    }

    [Authorize(Roles = "Admin, Doctor")]
    [HttpPost("register-patient")]
    [ProducesResponseType(200, Type = typeof(UserDto))]
    public async Task<IActionResult> RegisterPatient([FromBody] PatientRegisterRequest patientRegisterRequest)
    {
        var patient = _mapper.Map<User>(patientRegisterRequest);
        var patientResult = await _authenticationService.RegisterPatientAsync(patient);
        return Ok(_mapper.Map<UserDto>(patientResult));
    }


    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest forgotPasswordRequest)
    {
        await _authenticationService.ForgotPassword(forgotPasswordRequest);
        return Ok();
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto resetPasswordRequestDto)
    {
        var existingUser = _userService.GetByEmail(resetPasswordRequestDto.Email);
        if (existingUser == null) return NotFound($"User with email {resetPasswordRequestDto.Email} not found");

        if (existingUser.PasswordResetCode != resetPasswordRequestDto.PasswordResetCode) return BadRequest("Reset codes don't match");

        await _authenticationService.ResetPassword(existingUser, resetPasswordRequestDto.Password);
        return Ok();
    }

    // this is used for the patient when he first logs in and wants to change his password
    [HttpPost("change-default-password")]
    public async Task<IActionResult> ChangeDefaultPassword([FromBody] ChangeDefaultPasswordDto changeDefaultPasswordDto)
    {
        var user = await _userService.GetByIdAsync(changeDefaultPasswordDto.UserId);
        if (user == null) return NotFound();

        if (changeDefaultPasswordDto.Password != changeDefaultPasswordDto.ConfirmPassword) return BadRequest();
        await _authenticationService.ResetPassword(user, changeDefaultPasswordDto.Password);

        return Ok();
    }
}