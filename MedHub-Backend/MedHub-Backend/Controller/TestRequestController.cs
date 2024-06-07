using AutoMapper;
using MedHub_Backend.Dto;
using MedHub_Backend.Model;
using MedHub_Backend.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MedHub_Backend.Controller;

[ApiController]
[Route("api/v1/[controller]")]
public class TestRequestController(
    ITestRequestService testRequestService,
    ITestTypeService testTypeService,
    IMapper mapper
) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(List<TestRequestDto>))]
    public async Task<IActionResult> GetAllTestRequests()
    {
        var testRequests = await testRequestService.GetAllTestRequestsAsync();
        var testRequestsDto = mapper.Map<List<TestRequestDto>>(testRequests);
        return Ok(testRequestsDto);
    }

    [HttpGet("{testRequestId}")]
    [ProducesResponseType(200, Type = typeof(TestRequestDto))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetTestRequestById([FromRoute] int testRequestId)
    {
        var testRequest = await testRequestService.GetTestRequestByIdAsync(testRequestId);
        if (testRequest == null)
        {
            return NotFound($"Test request with id {testRequestId} not found");
        }

        return Ok(mapper.Map<TestRequestDto>(testRequest));
    }

    [HttpPost]
    [ProducesResponseType(201, Type = typeof(AddTestRequestDto))]
    public async Task<IActionResult> CreateTestRequest([FromBody] AddTestRequestDto testRequestDto)
    {
        var testRequest = mapper.Map<TestRequest>(testRequestDto);

        var createdTestRequest = await testRequestService.CreateNewTestRequestAsync(testRequest);

        var testTypes = await testTypeService.GetTestTypesFromIdList(testRequestDto.TestTypesId);

        createdTestRequest = await testRequestService.AddTestTypesAsync(createdTestRequest, testTypes);

        return CreatedAtAction(nameof(GetTestRequestById), new { testRequestId = createdTestRequest.Id }, mapper.Map<TestRequestDto>(createdTestRequest));
    }

    [HttpPut("{testRequestId}")]
    [ProducesResponseType(200, Type = typeof(TestRequestDto))]
    public async Task<IActionResult> UpdateTestRequest([FromRoute] int testRequestId, [FromBody] TestRequestDto testRequestDto)
    {
        if (testRequestId != testRequestDto.Id)
        {
            return BadRequest();
        }

        var existingTestRequest = await testRequestService.GetTestRequestByIdAsync(testRequestId);
        if (existingTestRequest == null)
        {
            return NotFound($"Patient with id {testRequestId} not found");
        }

        var testRequest = mapper.Map<TestRequest>(testRequestDto);
        var updatedTestRequest = await testRequestService.UpdateTestRequestAsync(testRequest);
        return Ok(mapper.Map<TestRequestDto>(updatedTestRequest));
    }

    [HttpDelete("{testRequestId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteTestRequest([FromRoute] int testRequestId)
    {
        var result = await testRequestService.DeleteTestRequestAsync(testRequestId);
        if (!result)
        {
            return NotFound($"Test request with id {testRequestId} not found");
        }

        return NoContent();
    }
}