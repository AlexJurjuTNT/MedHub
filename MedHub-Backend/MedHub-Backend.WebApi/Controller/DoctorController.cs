using AutoMapper;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Medhub_Backend.Application.Dtos.User;
using Medhub_Backend.Application.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedHub_Backend.WebApi.Controller;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class DoctorController : ControllerBase
{
    private readonly IDoctorService _doctorService;
    private readonly IMapper _mapper;

    public DoctorController(IDoctorService doctorService, IMapper mapper)
    {
        _doctorService = doctorService;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(LoadResult))]
    public async Task<IActionResult> GetAllDoctors([FromQuery] DataSourceLoadOptions loadOptions)
    {
        var doctors = _doctorService.GetAllDoctorsAsync();
        var loadedDoctors = await DataSourceLoader.LoadAsync(doctors, loadOptions);

        loadedDoctors.data = _mapper.Map<List<UserDto>>(loadedDoctors.data);

        return Ok(loadedDoctors);
    }

    [HttpGet("{doctorId}")]
    [ProducesResponseType(200, Type = typeof(UserDto))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetDoctorById([FromRoute] int doctorId)
    {
        var doctor = await _doctorService.GetDoctorById(doctorId);
        if (doctor == null) return NotFound($"Doctor with id {doctorId} not found");

        return Ok(_mapper.Map<UserDto>(doctor));
    }

    [HttpDelete("{doctorId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteDoctor([FromRoute] int doctorId)
    {
        var result = await _doctorService.DeleteDoctorAsync(doctorId);
        if (!result) return NotFound($"Doctor with id {doctorId} not found");

        return NoContent();
    }

    [HttpPut("{doctorId}")]
    [ProducesResponseType(200, Type = typeof(UserDto))]
    public async Task<IActionResult> UpdateDoctor([FromRoute] int doctorId, [FromBody] UpdateUserRequest updateUserRequest)
    {
        if (doctorId != updateUserRequest.Id) return BadRequest();

        var existingUser = await _doctorService.GetDoctorById(doctorId);
        if (existingUser == null) return NotFound($"User with id {doctorId} not found");

        existingUser.Email = updateUserRequest.Email;
        existingUser.Username = updateUserRequest.Username;
        existingUser.ClinicId = updateUserRequest.ClinicId;

        var updatedUser = await _doctorService.UpdateDoctorAsync(existingUser);
        var updatedUserDto = _mapper.Map<UserDto>(updatedUser);

        return Ok(updatedUserDto);
    }
}