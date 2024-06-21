using AutoMapper;
using MedHub_Backend.Dto.User;
using MedHub_Backend.Model;
using MedHub_Backend.Service.User;
using Microsoft.AspNetCore.Mvc;

namespace MedHub_Backend.Controller;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController(
    IUserService userService,
    IMapper mapper
) : ControllerBase
{
    /// <summary>
    ///     Get all users
    /// </summary>
    /// <returns>List of all users</returns>
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(List<UserDto>))]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await userService.GetAllUsersAsync();
        var usersDto = mapper.Map<List<UserDto>>(users);
        return Ok(usersDto);
    }

    /// <summary>
    ///     Get user by ID
    /// </summary>
    /// <param name="userId">ID of the user</param>
    /// <response code="200">User with the given ID</response>
    /// <response code="404">If the user is not found</response>
    [HttpGet("{userId}")]
    [ProducesResponseType(200, Type = typeof(UserDto))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetUserById([FromRoute] int userId)
    {
        var user = await userService.GetUserByIdAsync(userId);
        if (user == null) return NotFound($"User with id {userId} not found");

        return Ok(mapper.Map<UserDto>(user));
    }

    /// <summary>
    ///     Create a new user
    /// </summary>
    /// <param name="userDto">User to be created</param>
    /// <returns>Created user</returns>
    [HttpPost]
    [ProducesResponseType(201, Type = typeof(UserDto))]
    public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
    {
        var user = mapper.Map<User>(userDto);
        var createdUser = await userService.CreateUserAsync(user);
        return CreatedAtAction(nameof(GetUserById), new { userId = createdUser.Id }, mapper.Map<UserDto>(createdUser));
    }

    /// <summary>
    ///     Update an existing user
    /// </summary>
    /// <param name="userId">ID of the user to be updated</param>
    /// <param name="userDto">Updated user</param>
    /// <returns>Updated user</returns>
    [HttpPut("{userId}")]
    [ProducesResponseType(200, Type = typeof(UserDto))]
    public async Task<IActionResult> UpdateUser([FromRoute] int userId, [FromBody] UserDto userDto)
    {
        if (userId != userDto.Id) return BadRequest();

        var existingUser = await userService.GetUserByIdAsync(userId);
        if (existingUser == null) return NotFound($"User with id {userId} not found");

        var user = mapper.Map<User>(userDto);
        var updatedUser = await userService.UpdateUserAsync(user);
        return Ok(mapper.Map<UserDto>(updatedUser));
    }

    /// <summary>
    ///     Delete a user
    /// </summary>
    /// <param name="userId">ID of the user to be deleted</param>
    /// <returns>No content</returns>
    /// <response code="404">If the user is not found</response>
    [HttpDelete("{userId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteUser([FromRoute] int userId)
    {
        var result = await userService.DeleteUserAsync(userId);
        if (!result) return NotFound($"User with id {userId} not found");

        return NoContent();
    }
}