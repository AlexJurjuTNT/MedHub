using AutoMapper;
using MedHub_Backend.Dto;
using MedHub_Backend.Model;
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
        if (doctor == null) return NotFound();
        return Ok(mapper.Map<UserDto>(doctor));
    }

    [HttpDelete("{doctorId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteDoctor([FromRoute] int doctorId)
    {
        var result = await doctorService.DeleteDoctorAsync(doctorId);
        if (!result) return NotFound();
        return NoContent();
    }

    [HttpPut("{doctorId}")]
    [ProducesResponseType(200, Type = typeof(UserDto))]
    public async Task<IActionResult> UpdateDoctor([FromRoute] int doctorId, [FromBody] UserDto userDto)
    {
        var doctor = mapper.Map<User>(userDto);
        doctor.Id = doctorId;
        var updatedDoctor = await doctorService.UpdateDoctorAsync(doctor);
        return Ok(mapper.Map<UserDto>(updatedDoctor));
    }
}