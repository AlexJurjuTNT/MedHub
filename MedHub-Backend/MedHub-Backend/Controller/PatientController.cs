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
    IUserService userService,
    IMapper mapper
) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(200, Type = typeof(PatientDto))]
    public async Task<IActionResult> AddPatientInformation(AddPatientDataDto addPatientDataDto)
    {
        var patient = mapper.Map<Patient>(addPatientDataDto);
        var createdPatient = await patientService.CreatePatientAsync(patient);
        return Ok(mapper.Map<PatientDto>(createdPatient));
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(List<PatientDto>))]
    public async Task<IActionResult> GetAllPatientsInformation()
    {
        var patients = await patientService.GetAllPatientsAsync();
        return Ok(mapper.Map<List<PatientDto>>(patients));
    }

    [HttpPut("{patientId}")]
    [ProducesResponseType(200, Type = typeof(PatientDto))]
    public async Task<IActionResult> UpdatePatient([FromRoute] int patientId, [FromBody] PatientDto patientDto)
    {
        var patient = mapper.Map<Patient>(patientDto);
        patient.Id = patientId;
        var updatedPatient = await patientService.UpdatePatientAsync(patient);
        return Ok(mapper.Map<PatientDto>(updatedPatient));
    }

    [HttpDelete("{patientId}")]
    public async Task<IActionResult> DeletePatient([FromRoute] int patientId)
    {
        var result = await patientService.DeletePatientAsync(patientId);
        if (!result) return NotFound();
        return NoContent();
    }

    [HttpGet("{userId}")]
    [ProducesResponseType(200, Type = typeof(PatientDto))]
    public async Task<IActionResult> GetPatientInformationForUser([FromRoute] int userId)
    {
        var user = await userService.GetUserByIdAsync(userId);
        if (user == null) return NotFound();

        return Ok(mapper.Map<PatientDto>(user.Patient));
    }
}