using AutoMapper;
using Medhub_Backend.Application.Abstractions.Service;
using Medhub_Backend.Application.Dtos.Patient;
using Medhub_Backend.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedHub_Backend.WebApi.Controller;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class PatientInformationController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IPatientService _patientService;
    private readonly IUserService _userService;

    public PatientInformationController(IPatientService patientService, IUserService userService, IClinicService clinicService, IMapper mapper)
    {
        _patientService = patientService;
        _userService = userService;
        _mapper = mapper;
    }

    [Authorize(Roles = "Admin, Doctor")]
    [HttpPost]
    [ProducesResponseType(200, Type = typeof(PatientInformationDto))]
    public async Task<IActionResult> AddPatientInformation(CreatePatientInformationRequest createPatientInformationRequest)
    {
        var userResult = await _userService.GetByIdAsync(createPatientInformationRequest.UserId);
        if (userResult == null) return NotFound($"User with id {createPatientInformationRequest.UserId} not found");
        if (userResult.Role.Name != "Patient") return BadRequest($"User with id {createPatientInformationRequest.UserId} is not a patient");

        var patient = _mapper.Map<PatientInformation>(createPatientInformationRequest);
        var createdPatient = await _patientService.CreateAsync(patient);
        return Ok(_mapper.Map<PatientInformationDto>(createdPatient));
    }

    [Authorize(Roles = "Admin, Doctor")]
    [HttpPut("{patientId}")]
    [ProducesResponseType(200, Type = typeof(PatientInformationDto))]
    public async Task<IActionResult> UpdatePatientInformation([FromRoute] int patientId, [FromBody] UpdatePatientInformationRequest informationRequest)
    {
        var existingPatient = await _patientService.GetByIdAsync(patientId);
        if (existingPatient == null) return NotFound();

        _mapper.Map(informationRequest, existingPatient);

        var updatedPatient = await _patientService.UpdateAsync(existingPatient);
        var updatedPatientDto = _mapper.Map<PatientInformationDto>(updatedPatient);

        return Ok(updatedPatientDto);
    }

    [Authorize(Roles = "Admin, Doctor")]
    [HttpDelete("{patientId}")]
    public async Task<IActionResult> DeletePatientInformation([FromRoute] int patientId)
    {
        var result = await _patientService.DeleteAsync(patientId);
        if (!result) return NotFound($"Patient with id ${patientId} not found");

        return NoContent();
    }
    
    [HttpGet("{userId}")]
    [ProducesResponseType(200, Type = typeof(PatientInformationDto))]
    public async Task<IActionResult> GetPatientInformationForUser([FromRoute] int userId)
    {
        var user = await _userService.GetByIdAsync(userId);
        if (user == null) return NotFound($"User with id {userId} not found");
        if (user.Role.Name != "Patient") return BadRequest($"User with id {userId} is not a patient");

        var patientDto = _mapper.Map<PatientInformationDto>(user.PatientInformation);
        return Ok(patientDto);
    }
}