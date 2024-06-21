using AutoMapper;
using MedHub_Backend.Dto.Laboratory;
using MedHub_Backend.Model;
using MedHub_Backend.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MedHub_Backend.Controller;

[ApiController]
[Route("api/v1/[controller]")]
public class LaboratoryController(
    ILaboratoryService laboratoryService,
    ITestTypeService testTypeService,
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
    public async Task<IActionResult> CreateLaboratory([FromBody] AddLaboratoryDto laboratoryDto)
    {
        try
        {
            var laboratory = mapper.Map<Laboratory>(laboratoryDto);
            var testTypes = await testTypeService.GetTestTypesFromIdList(laboratoryDto.TestTypesId);
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
    public async Task<IActionResult> UpdateLaboratory([FromRoute] int laboratoryId, [FromBody] AddLaboratoryDto laboratoryDto)
    {
        var existingLaboratory = await laboratoryService.GetLaboratoryByIdAsync(laboratoryId);
        if (existingLaboratory == null) return NotFound();

        var testTypes = await testTypeService.GetTestTypesFromIdList(laboratoryDto.TestTypesId);

        existingLaboratory.ClinicId = laboratoryDto.ClinicId;
        existingLaboratory.Location = laboratoryDto.Location;
        // clear needs to be added because of proxies, without clear the testTypes will not be changed
        existingLaboratory.TestTypes.Clear();
        existingLaboratory.TestTypes = testTypes;

        var updatedLaboratory = await laboratoryService.UpdateLaboratoryAsync(existingLaboratory);
        return Ok(mapper.Map<LaboratoryDto>(updatedLaboratory));
    }
}