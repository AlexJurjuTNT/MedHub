using AutoMapper;
using MedHub_Backend.Dto.TestType;
using MedHub_Backend.Model;
using MedHub_Backend.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MedHub_Backend.Controller;

[ApiController]
[Route("api/v1/[controller]")]
public class TestTypeController(
    ITestTypeService testTypeService,
    IMapper mapper
) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(List<TestTypeDto>))]
    public async Task<IActionResult> GetAllTestTypes()
    {
        var testTypes = await testTypeService.GetAllTestTypesAsync();
        return Ok(mapper.Map<List<TestTypeDto>>(testTypes));
    }

    [HttpGet("{testTypeId}")]
    [ProducesResponseType(200, Type = typeof(TestTypeDto))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetTestTypeById([FromRoute] int testTypeId)
    {
        var testType = await testTypeService.GetTestTypeByIdAsync(testTypeId);
        if (testType == null) return NotFound($"TestType with id {testTypeId} not found");

        return Ok(mapper.Map<TestTypeDto>(testType));
    }

    [HttpPost]
    [ProducesResponseType(201, Type = typeof(TestTypeDto))]
    public async Task<IActionResult> CreateTestType([FromBody] AddTestTypeDto testTypeDto)
    {
        var testType = mapper.Map<TestType>(testTypeDto);
        var createdTestType = await testTypeService.CreateTestTypeAsync(testType);
        return CreatedAtAction(nameof(GetTestTypeById), new { testTypeId = createdTestType.Id }, mapper.Map<TestTypeDto>(createdTestType));
    }

    [HttpPut("{testTypeId}")]
    [ProducesResponseType(200, Type = typeof(TestTypeDto))]
    public async Task<IActionResult> UpdateTestType([FromRoute] int testTypeId, [FromBody] TestTypeDto testTypeDto)
    {
        if (testTypeId != testTypeDto.Id) return BadRequest();

        var existingTestType = await testTypeService.GetTestTypeByIdAsync(testTypeId);
        if (existingTestType == null) return NotFound($"TestType with id {testTypeId} not found");

        var testType = mapper.Map<TestType>(testTypeDto);
        var updatedTestType = await testTypeService.UpdateTestTypeAsync(testType);
        return Ok(mapper.Map<TestTypeDto>(updatedTestType));
    }


    [HttpDelete("{testTypeId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteTestType([FromRoute] int testTypeId)
    {
        var result = await testTypeService.DeleteClinicByIdAsync(testTypeId);
        if (!result) return NotFound($"TestType with id {testTypeId} not found");

        return NoContent();
    }
}