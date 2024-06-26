using AutoMapper;
using Medhub_Backend.Business.Dtos.Laboratory;
using Medhub_Backend.Business.Service.Interface;
using Medhub_Backend.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace MedHub_Backend.WebApi.Controller;

[ApiController]
[Route("api/v1/[controller]")]
public class LaboratoryController(
    ILaboratoryService laboratoryService,
    ITestTypeService testTypeService,
    IClinicService clinicService,
    IMapper mapper
)
    : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(List<LaboratoryDto>))]
    public async Task<IActionResult> GetAllLaboratories()
    {
        var laboratories = await laboratoryService.GetAllLaboratoriesAsync();
        var laboratoriesDtos = mapper.Map<List<LaboratoryDto>>(laboratories);
        return Ok(laboratoriesDtos);
    }

    [HttpGet("{laboratoryId}")]
    [ProducesResponseType(200, Type = typeof(LaboratoryDto))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetLaboratoryById([FromRoute] int laboratoryId)
    {
        var laboratory = await laboratoryService.GetLaboratoryByIdAsync(laboratoryId);
        if (laboratory == null) return NotFound($"Laboratory with id {laboratoryId} not found");

        var laboratoryDto = mapper.Map<LaboratoryDto>(laboratory);
        return Ok(laboratoryDto);
    }

    [HttpPost]
    [ProducesResponseType(201, Type = typeof(LaboratoryDto))]
    public async Task<IActionResult> CreateLaboratory([FromBody] CreateLaboratoryRequest createLaboratoryRequest)
    {
        var clinic = await clinicService.GetClinicByIdAsync(createLaboratoryRequest.ClinicId);
        if (clinic is null) return NotFound();

        try
        {
            var laboratory = mapper.Map<Laboratory>(createLaboratoryRequest);
            var testTypes = await testTypeService.GetTestTypesFromIdList(createLaboratoryRequest.TestTypesId);
            var createdLaboratory = await laboratoryService.CreateLaboratoryAsync(laboratory, testTypes);
            return CreatedAtAction(nameof(GetLaboratoryById), new { laboratoryId = createdLaboratory.Id }, mapper.Map<LaboratoryDto>(createdLaboratory));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{laboratoryId}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> DeleteLaboratory([FromRoute] int laboratoryId)
    {
        var result = await laboratoryService.DeleteLaboratoryAsync(laboratoryId);
        if (result == false) return NotFound($"Laboratory with id {laboratoryId} not found");

        return NoContent();
    }

    [HttpPut("{laboratoryId}")]
    [ProducesResponseType(200, Type = typeof(LaboratoryDto))]
    public async Task<IActionResult> UpdateLaboratory([FromRoute] int laboratoryId, [FromBody] UpdateLaboratoryRequest updateLaboratoryRequest)
    {
        var existingLaboratory = await laboratoryService.GetLaboratoryByIdAsync(laboratoryId);
        if (existingLaboratory == null) return NotFound();

        var testTypes = await testTypeService.GetTestTypesFromIdList(updateLaboratoryRequest.TestTypesId);

        existingLaboratory.ClinicId = updateLaboratoryRequest.ClinicId;
        existingLaboratory.Location = updateLaboratoryRequest.Location;
        // clear needs to be added because of proxies, without clear the testTypes will not be changed
        existingLaboratory.TestTypes.Clear();
        existingLaboratory.TestTypes = testTypes;

        var updatedLaboratory = await laboratoryService.UpdateLaboratoryAsync(existingLaboratory);
        return Ok(mapper.Map<LaboratoryDto>(updatedLaboratory));
    }
}