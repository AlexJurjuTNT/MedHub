using AutoMapper;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Medhub_Backend.Application.Abstractions.Service;
using Medhub_Backend.Application.Dtos.Laboratory;
using Medhub_Backend.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedHub_Backend.WebApi.Controller;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class LaboratoryController : ControllerBase
{
    private readonly IClinicService _clinicService;
    private readonly ILaboratoryService _laboratoryService;
    private readonly IMapper _mapper;
    private readonly ITestTypeService _testTypeService;

    public LaboratoryController(ILaboratoryService laboratoryService, ITestTypeService testTypeService, IClinicService clinicService, IMapper mapper)
    {
        _laboratoryService = laboratoryService;
        _testTypeService = testTypeService;
        _clinicService = clinicService;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(LoadResult))]
    public async Task<IActionResult> GetAllLaboratories([FromQuery] DataSourceLoadOptions loadOptions)
    {
        var laboratories = _laboratoryService.GetAllAsync();
        var loadedLaboratories = await DataSourceLoader.LoadAsync(laboratories, loadOptions);
        loadedLaboratories.data = _mapper.Map<List<LaboratoryDto>>(loadedLaboratories.data);
        return Ok(loadedLaboratories);
    }

    [HttpGet("{laboratoryId}")]
    [ProducesResponseType(200, Type = typeof(LaboratoryDto))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetLaboratoryById([FromRoute] int laboratoryId)
    {
        var laboratory = await _laboratoryService.GetByIdAsync(laboratoryId);
        if (laboratory == null) return NotFound($"Laboratory with id {laboratoryId} not found");

        var laboratoryDto = _mapper.Map<LaboratoryDto>(laboratory);
        return Ok(laboratoryDto);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ProducesResponseType(201, Type = typeof(LaboratoryDto))]
    public async Task<IActionResult> CreateLaboratory([FromBody] CreateLaboratoryRequest createLaboratoryRequest)
    {
        var clinic = await _clinicService.GetByIdAsync(createLaboratoryRequest.ClinicId);
        if (clinic is null) return NotFound();

        var laboratory = _mapper.Map<Laboratory>(createLaboratoryRequest);
        Console.WriteLine(laboratory);
        var testTypes = await _testTypeService.GetTestTypesFromIdList(createLaboratoryRequest.TestTypesId);
        var createdLaboratory = await _laboratoryService.CreateAsync(laboratory, testTypes);
        return CreatedAtAction(nameof(GetLaboratoryById), new { laboratoryId = createdLaboratory.Id }, _mapper.Map<LaboratoryDto>(createdLaboratory));
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{laboratoryId}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> DeleteLaboratory([FromRoute] int laboratoryId)
    {
        var result = await _laboratoryService.DeleteAsync(laboratoryId);
        if (result == false) return NotFound($"Laboratory with id {laboratoryId} not found");

        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{laboratoryId}")]
    [ProducesResponseType(200, Type = typeof(LaboratoryDto))]
    public async Task<IActionResult> UpdateLaboratory([FromRoute] int laboratoryId, [FromBody] UpdateLaboratoryRequest updateLaboratoryRequest)
    {
        var existingLaboratory = await _laboratoryService.GetByIdAsync(laboratoryId);
        if (existingLaboratory == null) return NotFound();

        var testTypes = await _testTypeService.GetTestTypesFromIdList(updateLaboratoryRequest.TestTypesId);

        existingLaboratory.ClinicId = updateLaboratoryRequest.ClinicId;
        existingLaboratory.Location = updateLaboratoryRequest.Location;
        // clear needs to be added because of proxies, without clear the testTypes will not be changed
        existingLaboratory.TestTypes.Clear();
        existingLaboratory.TestTypes = testTypes;

        var updatedLaboratory = await _laboratoryService.UpdateAsync(existingLaboratory);
        var laboratoryDto = _mapper.Map<LaboratoryDto>(updatedLaboratory);
        return Ok(laboratoryDto);
    }
}