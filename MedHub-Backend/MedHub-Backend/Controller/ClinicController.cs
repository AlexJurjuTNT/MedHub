using AutoMapper;
using MedHub_Backend.Dto;
using MedHub_Backend.Model;
using MedHub_Backend.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MedHub_Backend.Controller;

[ApiController]
[Route("api/v1/[controller]")]
public class ClinicController(IClinicService clinicService, IMapper mapper)
    : ControllerBase
{
    private readonly IClinicService _clinicService = clinicService ?? throw new ArgumentNullException(nameof(clinicService));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    /// <summary>
    /// Retrieves all Clinics
    /// </summary>
    /// <returns>List of all clinics</returns>
    /// <response code="200">Successful response</response>
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(List<ClinicDto>))]
    public async Task<IActionResult> GetAllClinicsAsync()
    {
        var clinics = await _clinicService.GetAllClinicsAsync();
        var clinicsDto = _mapper.Map<List<ClinicDto>>(clinics);
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
        var clinic = await _clinicService.GetClinicByIdAsync(clinicId);

        if (clinic == null) return NotFound();

        return Ok(_mapper.Map<ClinicDto>(clinic));
    }

    /// <summary>
    /// Create a new clinic
    /// </summary>
    /// <param name="clinicDto">Clinic to be created</param>
    /// <returns>Created clinic</returns>
    [HttpPost]
    [ProducesResponseType(201, Type = typeof(ClinicDto))]
    public async Task<IActionResult> CreateClinicAsync([FromBody] ClinicDto clinicDto)
    {
        var clinic = _mapper.Map<Clinic>(clinicDto);
        var createdClinic = await _clinicService.CreateClinicAsync(clinic);
        return CreatedAtAction(nameof(GetClinicById), new { clinicId = createdClinic.Id }, _mapper.Map<ClinicDto>(createdClinic));
    }

    /// <summary>
    /// Update an existing clinic
    /// </summary>
    /// <param name="clinicId">ID of the clinic to be updated</param>
    /// <param name="clinicDto">Updated clinic</param>
    /// <returns>Updated clinic</returns>
    [HttpPut("{clinicId}")]
    [ProducesResponseType(200, Type = typeof(ClinicDto))]
    public async Task<IActionResult> UpdateClinicAsync([FromRoute] int clinicId, [FromBody] ClinicDto clinicDto)
    {
        var clinic = _mapper.Map<Clinic>(clinicDto);
        clinic.Id = clinicId;
        var updatedClinic = await _clinicService.UpdateClinicAsync(clinic);
        return Ok(_mapper.Map<ClinicDto>(updatedClinic));
    }

    /// <summary>
    /// Delete a clinic
    /// </summary>
    /// <param name="clinicId">ID of the clinic to be deleted</param>
    /// <returns>No content</returns>
    [HttpDelete("{clinicId}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> DeleteClinicAsync([FromRoute] int clinicId)
    {
        var result = await _clinicService.DeleteClinicByIdAsync(clinicId);
        if (!result) return NotFound();
        return NoContent();
    }
}