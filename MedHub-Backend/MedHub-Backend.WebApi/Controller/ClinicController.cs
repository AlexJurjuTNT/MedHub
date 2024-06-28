using AutoMapper;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Medhub_Backend.Application.Abstractions.Service;
using Medhub_Backend.Application.Dtos.Clinic;
using Medhub_Backend.Application.Dtos.Laboratory;
using Medhub_Backend.Application.Dtos.User;
using Medhub_Backend.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedHub_Backend.WebApi.Controller;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class ClinicController : ControllerBase
{
    private readonly IClinicService _clinicService;
    private readonly IMapper _mapper;

    public ClinicController(IClinicService clinicService, IMapper mapper)
    {
        _clinicService = clinicService;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(LoadResult))]
    public async Task<IActionResult> GetAllClinics([FromQuery] DataSourceLoadOptions loadOptions)
    {
        var clinics = _clinicService.GetAll();
        var loadedClinics = await DataSourceLoader.LoadAsync(clinics, loadOptions);
        loadedClinics.data = _mapper.Map<List<ClinicDto>>(loadedClinics.data);
        return Ok(loadedClinics);
    }

    [HttpGet("{clinicId}")]
    [ProducesResponseType(200, Type = typeof(ClinicDto))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetClinicById([FromRoute] int clinicId)
    {
        var clinic = await _clinicService.GetByIdAsync(clinicId);
        if (clinic == null) return NotFound($"Clinic with id {clinicId} not found");

        var clinicDto = _mapper.Map<ClinicDto>(clinic);
        return Ok(clinicDto);
    }


    [HttpPost]
    [ProducesResponseType(201, Type = typeof(ClinicDto))]
    public async Task<IActionResult> CreateClinic([FromBody] AddClinicRequest clinicRequest)
    {
        var clinic = _mapper.Map<Clinic>(clinicRequest);
        var createdClinic = await _clinicService.CreateAsync(clinic);
        return CreatedAtAction(nameof(GetClinicById), new { clinicId = createdClinic.Id }, _mapper.Map<ClinicDto>(createdClinic));
    }

    [HttpPut("{clinicId}")]
    [ProducesResponseType(200, Type = typeof(ClinicDto))]
    public async Task<IActionResult> UpdateClinic([FromRoute] int clinicId, [FromBody] UpdateClinicRequest request)
    {
        if (clinicId != request.Id) return BadRequest();

        var existingClinic = await _clinicService.GetByIdAsync(clinicId);
        if (existingClinic == null) return NotFound();

        _mapper.Map(request, existingClinic);

        var updatedClinic = await _clinicService.UpdateAsync(existingClinic);
        var updatedClinicDto = _mapper.Map<ClinicDto>(updatedClinic);

        return Ok(updatedClinicDto);
    }

    [HttpDelete("{clinicId}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> DeleteClinic([FromRoute] int clinicId)
    {
        var result = await _clinicService.DeleteByIdAsync(clinicId);
        if (!result) return NotFound($"Clinic with id {clinicId} not found");

        return NoContent();
    }


    [HttpGet("{clinicId}/doctors")]
    [ProducesResponseType(200, Type = typeof(List<UserDto>))]
    public async Task<IActionResult> GetAllDoctorsOfClinic([FromRoute] int clinicId)
    {
        var clinic = await _clinicService.GetByIdAsync(clinicId);
        if (clinic == null) return NotFound($"Clinic with id {clinicId} not found");

        var patients = clinic.Users.Where(u => u.Role.Name == "Doctor").ToList();

        var userDtos = _mapper.Map<List<UserDto>>(patients);
        return Ok(userDtos);
    }

    [HttpGet("{clinicId}/laboratories")]
    [ProducesResponseType(200, Type = typeof(List<LaboratoryDto>))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetAllLaboratoriesOfClinic([FromRoute] int clinicId)
    {
        var clinic = await _clinicService.GetByIdAsync(clinicId);
        if (clinic == null) return NotFound($"Clinic with id {clinicId} not found");

        var laboratories = clinic.Laboratories;
        var laboratoriesDto = _mapper.Map<List<LaboratoryDto>>(laboratories);
        return Ok(laboratoriesDto);
    }
}