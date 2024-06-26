using AutoMapper;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Medhub_Backend.Business.Dtos.Clinic;
using Medhub_Backend.Business.Dtos.Laboratory;
using Medhub_Backend.Business.Dtos.User;
using Medhub_Backend.Business.Service.Interface;
using Medhub_Backend.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace MedHub_Backend.WebApi.Controller;

[ApiController]
[Route("api/v1/[controller]")]
public class ClinicController(
    IClinicService clinicService,
    IMapper mapper
) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(LoadResult))]
    public async Task<IActionResult> GetAllClinics([FromQuery] DataSourceLoadOptions loadOptions)
    {
        var clinics = clinicService.GetAllClinics();
        var resultingClinics = await DataSourceLoader.LoadAsync(clinics, loadOptions);

        resultingClinics.data = mapper.Map<List<ClinicDto>>(resultingClinics.data);

        return Ok(resultingClinics);
    }

    [HttpGet("{clinicId}")]
    [ProducesResponseType(200, Type = typeof(ClinicDto))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetClinicById([FromRoute] int clinicId)
    {
        var clinic = await clinicService.GetClinicByIdAsync(clinicId);
        if (clinic == null) return NotFound($"Clinic with id {clinicId} not found");

        return Ok(mapper.Map<ClinicDto>(clinic));
    }


    [HttpPost]
    [ProducesResponseType(201, Type = typeof(ClinicDto))]
    public async Task<IActionResult> CreateClinic([FromBody] AddClinicDto clinicDto)
    {
        var clinic = mapper.Map<Clinic>(clinicDto);
        var createdClinic = await clinicService.CreateClinicAsync(clinic);
        return CreatedAtAction(nameof(GetClinicById), new { clinicId = createdClinic.Id }, mapper.Map<ClinicDto>(createdClinic));
    }

    [HttpPut("{clinicId}")]
    [ProducesResponseType(200, Type = typeof(ClinicDto))]
    public async Task<IActionResult> UpdateClinic([FromRoute] int clinicId, [FromBody] UpdateClinicDto clinicDto)
    {
        if (clinicId != clinicDto.Id) return BadRequest();
        var clinic = mapper.Map<Clinic>(clinicDto);
        var updatedClinic = await clinicService.UpdateClinicAsync(clinic);
        return Ok(mapper.Map<ClinicDto>(updatedClinic));
    }

    [HttpDelete("{clinicId}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> DeleteClinic([FromRoute] int clinicId)
    {
        var result = await clinicService.DeleteClinicByIdAsync(clinicId);
        if (!result) return NotFound($"Clinic with id {clinicId} not found");

        return NoContent();
    }


    [HttpGet("{clinicId}/doctors")]
    [ProducesResponseType(200, Type = typeof(List<UserDto>))]
    public async Task<IActionResult> GetAllDoctorsOfClinic([FromRoute] int clinicId)
    {
        var clinic = await clinicService.GetClinicByIdAsync(clinicId);
        if (clinic == null) return NotFound($"Clinic with id {clinicId} not found");

        var patients = clinic.Users.Where(u => u.Role.Name == "Doctor").ToList();

        return Ok(mapper.Map<List<UserDto>>(patients));
    }

    [HttpGet("{clinicId}/laboratories")]
    [ProducesResponseType(200, Type = typeof(List<LaboratoryDto>))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetAllLaboratoriesOfClinic([FromRoute] int clinicId)
    {
        var clinic = await clinicService.GetClinicByIdAsync(clinicId);
        if (clinic == null) return NotFound($"Clinic with id {clinicId} not found");

        var laboratories = clinic.Laboratories;
        var laboratoriesDto = mapper.Map<List<LaboratoryDto>>(laboratories);
        return Ok(laboratoriesDto);
    }
}