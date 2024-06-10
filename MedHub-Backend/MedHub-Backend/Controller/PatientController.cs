using AutoMapper;
using MedHub_Backend.Dto;
using MedHub_Backend.Model;
using MedHub_Backend.Service.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
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
        var userResult = await userService.GetUserByIdAsync(addPatientDataDto.UserId);
        if (userResult == null)
        {
            return NotFound($"User with id {addPatientDataDto.UserId} not found");
        }

        if (userResult.Role.Name != "Patient")
        {
            return BadRequest($"User with id {addPatientDataDto.UserId} is not a patient");
        }

        var patient = mapper.Map<Patient>(addPatientDataDto);
        var createdPatient = await patientService.CreatePatientAsync(patient);
        return Ok(mapper.Map<PatientDto>(createdPatient));
    }

    [HttpGet("user-patients")]
    [ProducesResponseType(200, Type = typeof(List<UserDto>))]
    public async Task<IActionResult> GetAllUserPatients()
    {
        var patients = await patientService.GetAllUserPatientsAsync();
        return Ok(mapper.Map<List<UserDto>>(patients));
    }

    [HttpGet("patients-informations")]
    [ProducesResponseType(200, Type = typeof(List<PatientDto>))]
    public async Task<IActionResult> GetInformationsOfAllPatients()
    {
        var patients = await patientService.GetInformationsOfAllPatientsAsync();
        return Ok(mapper.Map<List<PatientDto>>(patients));
    }

    [HttpPut("{patientId}")]
    [ProducesResponseType(200, Type = typeof(PatientDto))]
    public async Task<IActionResult> UpdatePatient([FromRoute] int patientId, [FromBody] PatientDto patientDto)
    {
        if (patientId != patientDto.Id)
        {
            return BadRequest();
        }

        var existingPatient = await patientService.GetPatientAsync(patientId);
        if (existingPatient == null)
        {
            return NotFound($"Patient with id {patientId} not found");
        }

        var updatedPatient = await patientService.UpdatePatientAsync(mapper.Map<Patient>(patientDto));
        return Ok(mapper.Map<PatientDto>(updatedPatient));
    }

    [HttpDelete("{patientId}")]
    public async Task<IActionResult> DeletePatient([FromRoute] int patientId)
    {
        var result = await patientService.DeletePatientAsync(patientId);
        if (!result)
        {
            return NotFound($"Patient with id ${patientId} not found");
        }

        return NoContent();
    }

    [HttpGet("{userId}")]
    [ProducesResponseType(200, Type = typeof(PatientDto))]
    public async Task<IActionResult> GetPatientInformationForUser([FromRoute] int userId)
    {
        var user = await userService.GetUserByIdAsync(userId);
        if (user == null)
        {
            return NotFound($"User with id {userId} not found");
        }

        return Ok(mapper.Map<PatientDto>(user.Patient));
    }
}