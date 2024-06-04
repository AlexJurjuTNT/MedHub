using System.ComponentModel.DataAnnotations;

namespace MedHub_Backend.Dto;

public class RegisterRequestDto
{
    [Required(ErrorMessage = "Username is required")]
    [MaxLength(100, ErrorMessage = "Username can't exceed 100 characters")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [MaxLength(100, ErrorMessage = "Email can't exceed 100 characters")]
    [EmailAddress(ErrorMessage = "Invalid email address format")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [MaxLength(100, ErrorMessage = "Password cannot exceed 100 characters")]
    public string Password { get; set; }

    [Required(ErrorMessage = "The id of the clinic cannot be null")]
    public int ClinicId { get; set; }
}