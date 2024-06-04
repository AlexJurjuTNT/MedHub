using System.ComponentModel.DataAnnotations;

namespace MedHub_Backend.Dto;

public class UserDto
{
    [Required(ErrorMessage = "Id is required")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address format")]
    [MaxLength(100, ErrorMessage = "Email can't exceed 100 characters")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Username is required")]
    [MaxLength(100, ErrorMessage = "Username can't exceed 100 characters")]
    public string Username { get; set; }
}