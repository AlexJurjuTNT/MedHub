using AutoMapper;
using Medhub_Backend.Business.Dtos.User;
using Medhub_Backend.Business.Service.Interface;
using Medhub_Backend.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    [ProducesResponseType(200, Type = typeof(List<UserDto>))]
    public async Task<IActionResult> GetAllUsers()
    {
        var usersQuery = await _userService.GetAllUsersAsync();
        var users = await usersQuery.ToListAsync();
        var usersDto = _mapper.Map<List<UserDto>>(users);
        return Ok(usersDto);
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

        existingUser.Email = updateUserRequest.Email;
        existingUser.Username = updateUserRequest.Username;
        existingUser.ClinicId = updateUserRequest.ClinicId;

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