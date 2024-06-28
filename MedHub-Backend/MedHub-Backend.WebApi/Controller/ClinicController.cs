using AutoMapper;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Medhub_Backend.Application.Dtos.Clinic;
using Medhub_Backend.Application.Dtos.Laboratory;
using Medhub_Backend.Application.Dtos.User;
using Medhub_Backend.Application.Service.Interface;
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
        var clinics = _clinicService.GetAllClinics();
        var loadedClinics = await DataSourceLoader.LoadAsync(clinics, loadOptions);
        loadedClinics.data = _mapper.Map<List<ClinicDto>>(loadedClinics.data);
        return Ok(loadedClinics);
    }

    [HttpGet("{clinicId}")]
    [ProducesResponseType(200, Type = typeof(ClinicDto))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetClinicById([FromRoute] int clinicId)
    {
        var clinic = await _clinicService.GetClinicByIdAsync(clinicId);
        if (clinic == null) return NotFound($"Clinic with id {clinicId} not found");

        return Ok(_mapper.Map<ClinicDto>(clinic));
    }


    [HttpPost]
    [ProducesResponseType(201, Type = typeof(ClinicDto))]
    public async Task<IActionResult> CreateClinic([FromBody] AddClinicDto clinicDto)
    {
        var clinic = _mapper.Map<Clinic>(clinicDto);
        var createdClinic = await _clinicService.CreateClinicAsync(clinic);
        return CreatedAtAction(nameof(GetClinicById), new { clinicId = createdClinic.Id }, _mapper.Map<ClinicDto>(createdClinic));
    }

    [HttpPut("{clinicId}")]
    [ProducesResponseType(200, Type = typeof(ClinicDto))]
    public async Task<IActionResult> UpdateClinic([FromRoute] int clinicId, [FromBody] UpdateClinicDto clinicDto)
    {
        if (clinicId != clinicDto.Id) return BadRequest();
        var clinic = _mapper.Map<Clinic>(clinicDto);
        var updatedClinic = await _clinicService.UpdateClinicAsync(clinic);
        return Ok(_mapper.Map<ClinicDto>(updatedClinic));
    }

    [HttpDelete("{clinicId}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> DeleteClinic([FromRoute] int clinicId)
    {
        var result = await _clinicService.DeleteClinicByIdAsync(clinicId);
        if (!result) return NotFound($"Clinic with id {clinicId} not found");

        return NoContent();
    }


    [HttpGet("{clinicId}/doctors")]
    [ProducesResponseType(200, Type = typeof(List<UserDto>))]
    public async Task<IActionResult> GetAllDoctorsOfClinic([FromRoute] int clinicId)
    {
        var clinic = await _clinicService.GetClinicByIdAsync(clinicId);
        if (clinic == null) return NotFound($"Clinic with id {clinicId} not found");

        var patients = clinic.Users.Where(u => u.Role.Name == "Doctor").ToList();

        return Ok(_mapper.Map<List<UserDto>>(patients));
    }

    [HttpGet("{clinicId}/laboratories")]
    [ProducesResponseType(200, Type = typeof(List<LaboratoryDto>))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetAllLaboratoriesOfClinic([FromRoute] int clinicId)
    {
        var clinic = await _clinicService.GetClinicByIdAsync(clinicId);
        if (clinic == null) return NotFound($"Clinic with id {clinicId} not found");

        var laboratories = clinic.Laboratories;
        var laboratoriesDto = _mapper.Map<List<LaboratoryDto>>(laboratories);
        return Ok(laboratoriesDto);
    }
}