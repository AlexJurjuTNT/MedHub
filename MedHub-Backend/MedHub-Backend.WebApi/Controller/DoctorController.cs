using AutoMapper;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Medhub_Backend.Business.Dtos.User;
using Medhub_Backend.Business.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MedHub_Backend.WebApi.Controller;

[ApiController]
[Route("api/v1/[controller]")]
public class DoctorController(
    IDoctorService doctorService,
    IMapper mapper
) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(LoadResult))]
    public async Task<IActionResult> GetAllDoctors([FromQuery] DataSourceLoadOptions loadOptions)
    {
        var doctors = await doctorService.GetAllDoctorsAsync();
        var loadedDoctors = await DataSourceLoader.LoadAsync(doctors, loadOptions);

        loadedDoctors.data = mapper.Map<List<UserDto>>(loadedDoctors.data);

        return Ok(loadedDoctors);
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
    public async Task<IActionResult> UpdateDoctor([FromRoute] int doctorId, [FromBody] UpdateUserRequest updateUserRequest)
    {
        if (doctorId != updateUserRequest.Id) return BadRequest();

        var existingUser = await doctorService.GetDoctorById(doctorId);
        if (existingUser == null) return NotFound($"User with id {doctorId} not found");

        existingUser.Email = updateUserRequest.Email;
        existingUser.Username = updateUserRequest.Username;
        existingUser.ClinicId = updateUserRequest.ClinicId;

        var updatedUser = await doctorService.UpdateDoctorAsync(existingUser);
        var updatedUserDto = mapper.Map<UserDto>(updatedUser);

        return Ok(updatedUserDto);
    }
}