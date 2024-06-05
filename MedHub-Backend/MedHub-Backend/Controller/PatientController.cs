using AutoMapper;
using MedHub_Backend.Dto;
using MedHub_Backend.Model;
using MedHub_Backend.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MedHub_Backend.Controller;

[ApiController]
[Route("api/v1/[controller]")]
public class PatientController(
    IPatientService patientService,
    IMapper mapper
) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(List<PatientDto>))]
    public async Task<IActionResult> GetAllPatients()
    {
        var patients = await patientService.GetAllPatients();
        return Ok(mapper.Map<List<PatientDto>>(patients));
    }

    [HttpGet("{patientId}")]
    [ProducesResponseType(200, Type = typeof(PatientDto))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetPatientById([FromRoute] int patientId)
    {
        var patient = await patientService.GetPatientById(patientId);
        if (patient == null) return NotFound();

        return Ok(mapper.Map<PatientDto>(patient));
    }

    // [HttpPost]
    // [ProducesResponseType(201, Type = typeof(PatientDto))]
    // public async Task<IActionResult> CreatePatientAsync([FromBody] PatientDto patientDto)
    // {
    //     var patient = mapper.Map<Patient>(patientDto);
    //     var createdPatient = await patientService.CreatePatientAsync(patient);
    //     return CreatedAtAction(nameof(CreatePatientAsync), new { userId = patient.Id }, mapper.Map<PatientDto>(createdPatient));
    // }

    [HttpPut("{patientId}")]
    [ProducesResponseType(200, Type = typeof(PatientDto))]
    public async Task<IActionResult> UpdatePatient([FromRoute] int patientId, [FromBody] PatientDto patientDto)
    {
        var patient = mapper.Map<Patient>(patientDto);
        patient.Id = patientId;
        var updatedPatient = await patientService.UpdatePatientAsync(patient);
        return Ok(mapper.Map<PatientDto>(updatedPatient));
    }

    // todo: should this also delete the user?
    [HttpDelete("{patientId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeletePatient([FromRoute] int patientId)
    {
        var result = await patientService.DeletePatientAsync(patientId);
        if (!result) return NotFound();
        return NoContent();
    }
}