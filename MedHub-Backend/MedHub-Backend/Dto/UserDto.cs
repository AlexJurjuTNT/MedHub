using System.ComponentModel.DataAnnotations;

namespace MedHub_Backend.Dto;

public class UserDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Username { get; set; }
}