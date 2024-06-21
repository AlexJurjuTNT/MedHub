using AutoMapper;
using MedHub_Backend.Dto.User;
using MedHub_Backend.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MedHub_Backend.Controller;

[ApiController]
[Route("api/v1/[controller]")]
public class DoctorController(
    IDoctorService doctorService,
    IMapper mapper
) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(UserDto))]
    public async Task<IActionResult> GetAllDoctors()
    {
        var doctors = await doctorService.GetAllDoctorsAsync();
        return Ok(mapper.Map<List<UserDto>>(doctors));
    }

    [HttpGet("{doctorId}")]
    [ProducesResponseType(200, Type = typeof(UserDto))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetDoctorById([FromRoute] int doctorId)
    {
        var doctor = await doctorService.GetDoctorById(doctorId);
        if (doctor == null) return NotFound($"Doctor with id {doctorId} not found");

        return Ok(mapper.Map<UserDto>(doctor));
    }

    [HttpDelete("{doctorId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteDoctor([FromRoute] int doctorId)
    {
        var result = await doctorService.DeleteDoctorAsync(doctorId);
        if (!result) return NotFound($"Doctor with id {doctorId} not found");

        return NoContent();
    }

    [HttpPut("{doctorId}")]
    [ProducesResponseType(200, Type = typeof(UserDto))]
    public async Task<IActionResult> UpdateDoctor([FromRoute] int doctorId, [FromBody] UserDto doctorDto)
    {
        if (doctorId != doctorDto.Id) return BadRequest();

        var existingDoctor = await doctorService.GetDoctorById(doctorId);
        if (existingDoctor == null) return NotFound($"Doctor with id {doctorId} not found");

        existingDoctor.Email = doctorDto.Email;
        existingDoctor.Username = doctorDto.Username;
        existingDoctor.ClinicId = doctorDto.ClinicId;

        var updatedDoctor = await doctorService.UpdateDoctorAsync(existingDoctor);

        return Ok(mapper.Map<UserDto>(updatedDoctor));
    }
}