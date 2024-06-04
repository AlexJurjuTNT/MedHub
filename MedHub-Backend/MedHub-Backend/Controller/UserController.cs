using AutoMapper;
using MedHub_Backend.Dto;
using MedHub_Backend.Model;
using MedHub_Backend.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MedHub_Backend.Controller;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController(IUserService userService, IMapper mapper)
    : ControllerBase
{
    private readonly IUserService _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    /// <summary>
    /// Get all users
    /// </summary>
    /// <returns>List of all users</returns>
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(List<UserDto>))]
    public async Task<IActionResult> GetAllUsersAsync()
    {
        var users = await _userService.GetAllUsersAsync();
        var usersDto = _mapper.Map<List<UserDto>>(users);
        return Ok(usersDto);
    }

    /// <summary>
    /// Get user by ID
    /// </summary>
    /// <param name="userId">ID of the user</param>
    /// <response code="200">User with the given ID</response>
    /// <response code="404">If the user is not found</response>
    [HttpGet("{userId}")]
    [ProducesResponseType(200, Type = typeof(UserDto))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetUserById([FromRoute] int userId)
    {
        var user = await _userService.GetUserByIdAsync(userId);

        if (user == null) return NotFound();

        return Ok(_mapper.Map<UserDto>(user));
    }

    /// <summary>
    /// Create a new user
    /// </summary>
    /// <param name="userDto">User to be created</param>
    /// <returns>Created user</returns>
    [HttpPost]
    [ProducesResponseType(201, Type = typeof(UserDto))]
    public async Task<IActionResult> CreateUserAsync([FromBody] UserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        var createdUser = await _userService.CreateUserAsync(user);
        return CreatedAtAction(nameof(GetUserById), new { userId = createdUser.Id }, _mapper.Map<UserDto>(createdUser));
    }

    /// <summary>
    /// Update an existing user
    /// </summary>
    /// <param name="userId">ID of the user to be updated</param>
    /// <param name="userDto">Updated user</param>
    /// <returns>Updated user</returns>
    [HttpPut("{userId}")]
    [ProducesResponseType(200, Type = typeof(UserDto))]
    public async Task<IActionResult> UpdateUserAsync([FromRoute] int userId, [FromBody] UserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        user.Id = userId;
        var updatedUser = await _userService.UpdateUserAsync(user);
        return Ok(_mapper.Map<UserDto>(updatedUser));
    }

    /// <summary>
    /// Delete a user
    /// </summary>
    /// <param name="userId">ID of the user to be deleted</param>
    /// <returns>No content</returns>
    /// <response code="404">If the user is not found</response>
    [HttpDelete("{userId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteUserAsync([FromRoute] int userId)
    {
        var result = await _userService.DeleteUserAsync(userId);
        if (!result) return NotFound();
        return NoContent();
    }
}