using AutoMapper;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Medhub_Backend.Application.Abstractions.Service;
using Medhub_Backend.Application.Dtos.TestType;
using Medhub_Backend.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedHub_Backend.WebApi.Controller;

[Authorize(Roles = "Admin, Doctor")]
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
        var testTypes = _testTypeService.GetAllAsync();
        var loadedTestTypes = await DataSourceLoader.LoadAsync(testTypes, loadOptions);
        loadedTestTypes.data = _mapper.Map<List<TestTypeDto>>(loadedTestTypes.data);
        return Ok(loadedTestTypes);
    }

    [HttpGet("{testTypeId}")]
    [ProducesResponseType(200, Type = typeof(TestTypeDto))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetTestTypeById([FromRoute] int testTypeId)
    {
        var testType = await _testTypeService.GetByIdAsync(testTypeId);
        if (testType == null) return NotFound($"TestType with id {testTypeId} not found");

        var testTypeDto = _mapper.Map<TestTypeDto>(testType);
        return Ok(testTypeDto);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ProducesResponseType(201, Type = typeof(TestTypeDto))]
    public async Task<IActionResult> CreateTestType([FromBody] CreateTestTypeRequest testTypeRequest)
    {
        var testType = _mapper.Map<TestType>(testTypeRequest);
        var createdTestType = await _testTypeService.CreateAsync(testType);
        return CreatedAtAction(nameof(GetTestTypeById), new { testTypeId = createdTestType.Id }, _mapper.Map<TestTypeDto>(createdTestType));
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{testTypeId}")]
    [ProducesResponseType(200, Type = typeof(TestTypeDto))]
    public async Task<IActionResult> UpdateTestType([FromRoute] int testTypeId, [FromBody] UpdateTestTypeRequest request)
    {
        if (testTypeId != request.Id) return BadRequest();

        var existingTestType = await _testTypeService.GetByIdAsync(testTypeId);
        if (existingTestType == null) return NotFound();

        _mapper.Map(request, existingTestType);

        var updatedTestType = await _testTypeService.UpdateAsync(existingTestType);
        var updatedTestTypeDto = _mapper.Map<TestTypeDto>(updatedTestType);

        return Ok(updatedTestTypeDto);
    }


    [Authorize(Roles = "Admin")]
    [HttpDelete("{testTypeId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteTestType([FromRoute] int testTypeId)
    {
        var result = await _testTypeService.DeleteByIdAsync(testTypeId);
        if (!result) return NotFound($"TestType with id {testTypeId} not found");
        return NoContent();
    }
}