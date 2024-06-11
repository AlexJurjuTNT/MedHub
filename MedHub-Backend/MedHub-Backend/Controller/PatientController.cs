using AutoMapper;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using MedHub_Backend.Dto;
using MedHub_Backend.Exceptions;
using MedHub_Backend.Model;
using MedHub_Backend.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MedHub_Backend.Controller;

[ApiController]
[Route("api/v1/[controller]")]
public class PatientController(
    IPatientService patientService,
    IUserService userService,
    IClinicService clinicService,
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


    [HttpPut("{patientId}")]
    [ProducesResponseType(200, Type = typeof(PatientDto))]
    public async Task<IActionResult> UpdatePatientInformation([FromRoute] int patientId, [FromBody] PatientDto patientDto)
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
    public async Task<IActionResult> DeletePatientInformation([FromRoute] int patientId)
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


    [HttpGet("patients-paged")]
    [ProducesResponseType(200, Type = typeof(LoadResult))]
    public async Task<IActionResult> GetAllPatientsOfClinic([FromQuery] int clinicId, [FromQuery] DataSourceLoadOptionsBase loadOptions)
    {
        try
        {
            var users = await clinicService.GetAllPatientsOfClinicAsync(clinicId);
            var resultingUsers = DataSourceLoader.Load(mapper.Map<IEnumerable<UserDto>>(users), loadOptions);
            return Ok(resultingUsers);
        }
        catch (ClinicNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}