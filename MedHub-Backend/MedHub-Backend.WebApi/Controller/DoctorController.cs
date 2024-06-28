using AutoMapper;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Medhub_Backend.Application.Abstractions.Service;
using Medhub_Backend.Application.Dtos.User;
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
        var doctors = _doctorService.GetAllAsync();
        var loadedDoctors = await DataSourceLoader.LoadAsync(doctors, loadOptions);

        loadedDoctors.data = _mapper.Map<List<UserDto>>(loadedDoctors.data);

        return Ok(loadedDoctors);
    }

    [HttpGet("{doctorId}")]
    [ProducesResponseType(200, Type = typeof(UserDto))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetDoctorById([FromRoute] int doctorId)
    {
        var doctor = await _doctorService.GetByIdAsync(doctorId);
        if (doctor == null) return NotFound($"Doctor with id {doctorId} not found");

        var userDto = _mapper.Map<UserDto>(doctor);
        return Ok(userDto);
    }

    [HttpDelete("{doctorId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteDoctor([FromRoute] int doctorId)
    {
        var result = await _doctorService.DeleteAsync(doctorId);
        if (!result) return NotFound($"Doctor with id {doctorId} not found");

        return NoContent();
    }

    [HttpPut("{doctorId}")]
    [ProducesResponseType(200, Type = typeof(UserDto))]
    public async Task<IActionResult> UpdateDoctor([FromRoute] int doctorId, [FromBody] UpdateUserRequest request)
    {
        var existingUser = await _doctorService.GetByIdAsync(doctorId);
        if (existingUser == null) return NotFound();

        _mapper.Map(request, existingUser);

        var updatedUser = await _doctorService.UpdateAsync(existingUser);
        var updatedUserDto = _mapper.Map<UserDto>(updatedUser);

        return Ok(updatedUserDto);
    }
}