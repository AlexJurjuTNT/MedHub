using AutoMapper;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Medhub_Backend.Business.Dtos.Patient;
using Medhub_Backend.Business.Dtos.User;
using Medhub_Backend.Business.Service.Interface;
using Medhub_Backend.Domain.Entities;
using Medhub_Backend.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace MedHub_Backend.WebApi.Controller;

[ApiController]
[Route("api/v1/[controller]")]
public class PatientController : ControllerBase
{
    private readonly IClinicService _clinicService;
    private readonly IMapper _mapper;
    private readonly IPatientService _patientService;
    private readonly IUserService _userService;

    public PatientController(IPatientService patientService, IUserService userService, IClinicService clinicService, IMapper mapper)
    {
        _patientService = patientService;
        _userService = userService;
        _clinicService = clinicService;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(200, Type = typeof(PatientDto))]
    public async Task<IActionResult> AddPatientInformation(AddPatientDataDto addPatientDataDto)
    {
        var userResult = await _userService.GetUserByIdAsync(addPatientDataDto.UserId);
        if (userResult == null) return NotFound($"User with id {addPatientDataDto.UserId} not found");
        if (userResult.Role.Name != "Patient") return BadRequest($"User with id {addPatientDataDto.UserId} is not a patient");

        var patient = _mapper.Map<Patient>(addPatientDataDto);
        var createdPatient = await _patientService.CreatePatientAsync(patient);
        return Ok(_mapper.Map<PatientDto>(createdPatient));
    }

    [HttpGet("user-patients")]
    [ProducesResponseType(200, Type = typeof(List<UserDto>))]
    public async Task<IActionResult> GetAllUserPatients()
    {
        var patients = await _patientService.GetAllUserPatientsAsync();
        return Ok(_mapper.Map<List<UserDto>>(patients));
    }


    [HttpPut("{patientId}")]
    [ProducesResponseType(200, Type = typeof(PatientDto))]
    public async Task<IActionResult> UpdatePatientInformation([FromRoute] int patientId, [FromBody] UpdatePatientDto patientDto)
    {
        var existingPatient = await _patientService.GetPatientAsync(patientId);
        if (existingPatient == null) return NotFound($"Patient with id {patientId} not found");

        existingPatient.Cnp = patientDto.Cnp;
        existingPatient.Gender = patientDto.Gender;
        existingPatient.Height = patientDto.Height;
        existingPatient.Weight = patientDto.Weight;
        existingPatient.DateOfBirth = patientDto.DateOfBirth;

        var updatedPatient = await _patientService.UpdatePatientAsync(existingPatient);
        return Ok(_mapper.Map<PatientDto>(updatedPatient));
    }

    [HttpDelete("{patientId}")]
    public async Task<IActionResult> DeletePatientInformation([FromRoute] int patientId)
    {
        var result = await _patientService.DeletePatientAsync(patientId);
        if (!result) return NotFound($"Patient with id ${patientId} not found");

        return NoContent();
    }

    [HttpGet("{userId}")]
    [ProducesResponseType(200, Type = typeof(PatientDto))]
    public async Task<IActionResult> GetPatientInformationForUser([FromRoute] int userId)
    {
        var user = await _userService.GetUserByIdAsync(userId);
        if (user == null) return NotFound($"User with id {userId} not found");
        if (user.Role.Name != "Patient") return BadRequest($"User with id {userId} is not a patient");

        return Ok(_mapper.Map<PatientDto>(user.Patient));
    }


    [HttpGet("paged")]
    [ProducesResponseType(200, Type = typeof(LoadResult))]
    public async Task<IActionResult> GetAllPatientsOfClinic([FromQuery] int clinicId, [FromQuery] DataSourceLoadOptions loadOptions)
    {
        try
        {
            var users = await _clinicService.GetAllPatientsOfClinicAsync(clinicId);
            var resultingUsers = DataSourceLoader.Load(_mapper.Map<IEnumerable<UserDto>>(users), loadOptions);
            return Ok(resultingUsers);
        }
        catch (ClinicNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}