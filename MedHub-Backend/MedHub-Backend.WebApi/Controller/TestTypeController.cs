using AutoMapper;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Medhub_Backend.Application.Dtos.TestType;
using Medhub_Backend.Application.Service.Interface;
using Medhub_Backend.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedHub_Backend.WebApi.Controller;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class TestTypeController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ITestTypeService _testTypeService;

    public TestTypeController(ITestTypeService testTypeService, IMapper mapper)
    {
        _testTypeService = testTypeService;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(LoadResult))]
    public async Task<IActionResult> GetAllTestTypes([FromQuery] DataSourceLoadOptions loadOptions)
    {
        var testTypes = _testTypeService.GetAllTestTypesAsync();
        var loadedTestTypes = await DataSourceLoader.LoadAsync(testTypes, loadOptions);
        loadedTestTypes.data = _mapper.Map<List<TestTypeDto>>(loadedTestTypes.data);
        return Ok(loadedTestTypes);
    }

    [HttpGet("{testTypeId}")]
    [ProducesResponseType(200, Type = typeof(TestTypeDto))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetTestTypeById([FromRoute] int testTypeId)
    {
        var testType = await _testTypeService.GetTestTypeByIdAsync(testTypeId);
        if (testType == null) return NotFound($"TestType with id {testTypeId} not found");

        return Ok(_mapper.Map<TestTypeDto>(testType));
    }

    [HttpPost]
    [ProducesResponseType(201, Type = typeof(TestTypeDto))]
    public async Task<IActionResult> CreateTestType([FromBody] AddTestTypeDto testTypeDto)
    {
        var testType = _mapper.Map<TestType>(testTypeDto);
        var createdTestType = await _testTypeService.CreateTestTypeAsync(testType);
        return CreatedAtAction(nameof(GetTestTypeById), new { testTypeId = createdTestType.Id }, _mapper.Map<TestTypeDto>(createdTestType));
    }

    [HttpPut("{testTypeId}")]
    [ProducesResponseType(200, Type = typeof(TestTypeDto))]
    public async Task<IActionResult> UpdateTestType([FromRoute] int testTypeId, [FromBody] TestTypeDto testTypeDto)
    {
        if (testTypeId != testTypeDto.Id) return BadRequest();
        var testType = _mapper.Map<TestType>(testTypeDto);
        var updatedTestType = await _testTypeService.UpdateTestTypeAsync(testType);
        return Ok(_mapper.Map<TestTypeDto>(updatedTestType));
    }


    [HttpDelete("{testTypeId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteTestType([FromRoute] int testTypeId)
    {
        var result = await _testTypeService.DeleteTestTypeByIdAsync(testTypeId);
        if (!result) return NotFound($"TestType with id {testTypeId} not found");

        return NoContent();
    }
}