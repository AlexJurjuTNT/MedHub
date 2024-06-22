using AutoMapper;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Medhub_Backend.Business.Dtos.TestType;
using Medhub_Backend.Business.Service.Interface;
using Medhub_Backend.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace MedHub_Backend.WebApi.Controller;

[ApiController]
[Route("api/v1/[controller]")]
public class TestTypeController(
    ITestTypeService testTypeService,
    IMapper mapper
) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(LoadResult))]
    public async Task<IActionResult> GetAllTestTypes([FromQuery] DataSourceLoadOptions loadOptions)
    {
        var testTypes = testTypeService.GetAllTestTypes();
        var resultingTestTypes = await DataSourceLoader.LoadAsync(testTypes, loadOptions);

        resultingTestTypes.data = mapper.Map<List<TestTypeDto>>(resultingTestTypes.data);
        return Ok(resultingTestTypes);
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