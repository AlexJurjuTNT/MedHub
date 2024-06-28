using AutoMapper;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Medhub_Backend.Application.Dtos.User;
using Medhub_Backend.Application.Service.Interface;
using Medhub_Backend.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MedHub_Backend.WebApi.Controller;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public UserController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(LoadResult))]
    public async Task<IActionResult> GetAllUsers([FromQuery] DataSourceLoadOptions loadOptions)
    {
        var users = _userService.GetAllUsers();
        var loadedUsers = await DataSourceLoader.LoadAsync(users, loadOptions);
        loadedUsers.data = _mapper.Map<List<UserDto>>(loadedUsers.data);
        return Ok(loadedUsers);
    }

    [HttpGet("{userId}")]
    [ProducesResponseType(200, Type = typeof(UserDto))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetUserById([FromRoute] int userId)
    {
        var user = await _userService.GetUserByIdAsync(userId);
        if (user == null) return NotFound($"User with id {userId} not found");

        return Ok(_mapper.Map<UserDto>(user));
    }

    [HttpPost]
    [ProducesResponseType(201, Type = typeof(UserDto))]
    public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        var createdUser = await _userService.CreateUserAsync(user);
        return CreatedAtAction(nameof(GetUserById), new { userId = createdUser.Id }, _mapper.Map<UserDto>(createdUser));
    }


    [HttpPut("{userId}")]
    [ProducesResponseType(200, Type = typeof(UserDto))]
    public async Task<IActionResult> UpdateUser([FromRoute] int userId, [FromBody] UpdateUserRequest updateUserRequest)
    {
        var existingUser = await _userService.GetUserByIdAsync(userId);
        if (existingUser == null) return NotFound($"User with id {userId} not found");

        _mapper.Map(updateUserRequest, existingUser);

        var updatedUser = await _userService.UpdateUserAsync(existingUser);
        var updatedUserDto = _mapper.Map<UserDto>(updatedUser);

        return Ok(updatedUserDto);
    }

    [HttpDelete("{userId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteUser([FromRoute] int userId)
    {
        var result = await _userService.DeleteUserAsync(userId);
        if (!result) return NotFound($"User with id {userId} not found");

        return NoContent();
    }
}