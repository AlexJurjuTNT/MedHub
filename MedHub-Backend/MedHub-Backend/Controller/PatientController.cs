using AutoMapper;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using MedHub_Backend.Dto.Patient;
using MedHub_Backend.Dto.TestRequest;
using MedHub_Backend.Dto.User;
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
    ITestRequestService testRequestService,
    IMapper mapper
) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(200, Type = typeof(PatientDto))]
    public async Task<IActionResult> AddPatientInformation(AddPatientDataDto addPatientDataDto)
    {
        var userResult = await userService.GetUserByIdAsync(addPatientDataDto.UserId);
        if (userResult == null) return NotFound($"User with id {addPatientDataDto.UserId} not found");
        if (userResult.Role.Name != "Patient") return BadRequest($"User with id {addPatientDataDto.UserId} is not a patient");

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
    public async Task<IActionResult> UpdatePatientInformation([FromRoute] int patientId, [FromBody] UpdatePatientDto patientDto)
    {
        var existingPatient = await patientService.GetPatientAsync(patientId);
        if (existingPatient == null) return NotFound($"Patient with id {patientId} not found");

        existingPatient.Cnp = patientDto.Cnp;
        existingPatient.Gender = patientDto.Gender;
        existingPatient.Height = patientDto.Height;
        existingPatient.Weight = patientDto.Weight;
        existingPatient.DateOfBirth = patientDto.DateOfBirth;

        var updatedPatient = await patientService.UpdatePatientAsync(existingPatient);
        return Ok(mapper.Map<PatientDto>(updatedPatient));
    }

    [HttpDelete("{patientId}")]
    public async Task<IActionResult> DeletePatientInformation([FromRoute] int patientId)
    {
        var result = await patientService.DeletePatientAsync(patientId);
        if (!result) return NotFound($"Patient with id ${patientId} not found");

        return NoContent();
    }

    [HttpGet("{userId}")]
    [ProducesResponseType(200, Type = typeof(PatientDto))]
    public async Task<IActionResult> GetPatientInformationForUser([FromRoute] int userId)
    {
        var user = await userService.GetUserByIdAsync(userId);
        if (user == null) return NotFound($"User with id {userId} not found");
        if (user.Role.Name != "Patient") return BadRequest($"User with id {userId} is not a patient");

        return Ok(mapper.Map<PatientDto>(user.Patient));
    }


    [HttpGet("paged")]
    [ProducesResponseType(200, Type = typeof(LoadResult))]
    public async Task<IActionResult> GetAllPatientsOfClinic([FromQuery] int clinicId, [FromQuery] DataSourceLoadOptions loadOptions)
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

    [HttpGet("{patientId}/test-requests")]
    [ProducesResponseType(200, Type = typeof(List<TestRequestDto>))]
    public async Task<IActionResult> GetTestRequestsOfPatient([FromRoute] int patientId)
    {
        var userPatient = await userService.GetUserByIdAsync(patientId);
        if (userPatient == null) return NotFound($"User with id {patientId} not found");

        if (userPatient.Role.Name != "Patient") return BadRequest();

        var testRequests = await testRequestService.GetAllTestRequestsOfUserAsync(userPatient.Id);
        return Ok(mapper.Map<List<TestRequestDto>>(testRequests));
    }
}