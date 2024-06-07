using AutoMapper;
using MedHub_Backend.Dto;
using MedHub_Backend.Model;
using MedHub_Backend.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MedHub_Backend.Controller;

[ApiController]
[Route("api/v1/[controller]")]
public class ClinicController(
    IClinicService clinicService,
    IMapper mapper
) : ControllerBase
{
    /// <summary>
    /// Retrieves all Clinics
    /// </summary>
    /// <returns>List of all clinics</returns>
    /// <response code="200">Successful response</response>
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(List<ClinicDto>))]
    public async Task<IActionResult> GetAllClinics()
    {
        var clinics = await clinicService.GetAllClinicsAsync();
        var clinicsDto = mapper.Map<List<ClinicDto>>(clinics);
        return Ok(clinicsDto);
    }

    /// <summary>
    /// Get clinic by ID
    /// </summary>
    /// <param name="clinicId">ID of the clinic</param>
    /// <returns>Clinic with the given ID</returns>
    /// <response code="200">Clinic found successfully</response>
    /// <response code="404">If the clinic is not found</response>
    [HttpGet("{clinicId}")]
    [ProducesResponseType(200, Type = typeof(ClinicDto))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetClinicById([FromRoute] int clinicId)
    {
        var clinic = await clinicService.GetClinicByIdAsync(clinicId);
        if (clinic == null) return NotFound($"Clinic with id {clinicId} not found");
        return Ok(mapper.Map<ClinicDto>(clinic));
    }

    /// <summary>
    /// Create a new clinic
    /// </summary>
    /// <param name="clinicDto">Clinic to be created</param>
    /// <returns>Created clinic</returns>
    [HttpPost]
    [ProducesResponseType(201, Type = typeof(ClinicDto))]
    public async Task<IActionResult> CreateClinic([FromBody] AddClinicDto clinicDto)
    {
        var clinic = mapper.Map<Clinic>(clinicDto);
        var createdClinic = await clinicService.CreateClinicAsync(clinic);
        return CreatedAtAction(nameof(GetClinicById), new { clinicId = createdClinic.Id }, mapper.Map<ClinicDto>(createdClinic));
    }

    /// <summary>
    /// Update an existing clinic
    /// </summary>
    /// <param name="clinicId">ID of the clinic to be updated</param>
    /// <param name="clinicDto">Updated clinic</param>
    /// <returns>Updated clinic</returns>
    [HttpPut("{clinicId}")]
    [ProducesResponseType(200, Type = typeof(ClinicDto))]
    public async Task<IActionResult> UpdateClinic([FromRoute] int clinicId, [FromBody] ClinicDto clinicDto)
    {
        var clinic = mapper.Map<Clinic>(clinicDto);
        clinic.Id = clinicId;
        var updatedClinic = await clinicService.UpdateClinicAsync(clinic);
        return Ok(mapper.Map<ClinicDto>(updatedClinic));
    }

    /// <summary>
    /// Delete a clinic
    /// </summary>
    /// <param name="clinicId">ID of the clinic to be deleted</param>
    /// <returns>No content</returns>
    [HttpDelete("{clinicId}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> DeleteClinic([FromRoute] int clinicId)
    {
        var result = await clinicService.DeleteClinicByIdAsync(clinicId);
        if (!result) return NotFound();
        return NoContent();
    }

    /// <summary>
    /// Retrive all patients of a clinic
    /// </summary>
    /// <param name="clinicId">ID of the clinic where the patients are</param>
    /// <returns>List of all patients that belong to that clinic</returns>
    [HttpGet("{clinicId}/patients")]
    [ProducesResponseType(200, Type = typeof(List<UserDto>))]
    public async Task<IActionResult> GetAllPatientsOfClinic([FromRoute] int clinicId)
    {
        var clinic = await clinicService.GetClinicByIdAsync(clinicId);
        if (clinic == null) return NotFound($"Clinic with id {clinicId} not found");

        var patients = clinic.Users.Where(u => u.Role.Name == "Patient").ToList();

        return Ok(mapper.Map<List<UserDto>>(patients));
    }

    /// <summary>
    /// Retrive all doctors of a clinic
    /// </summary>
    /// <param name="clinicId">ID of the clinic where the doctors are</param>
    /// <returns>List of all doctors that belong to that clinic</returns>
    [HttpGet("{clinicId}/doctors")]
    [ProducesResponseType(200, Type = typeof(List<UserDto>))]
    public async Task<IActionResult> GetAllDoctorsOfClinic([FromRoute] int clinicId)
    {
        var clinic = await clinicService.GetClinicByIdAsync(clinicId);
        if (clinic == null) return NotFound();

        var patients = clinic.Users.Where(u => u.Role.Name == "Doctor").ToList();

        return Ok(mapper.Map<List<UserDto>>(patients));
    }
}