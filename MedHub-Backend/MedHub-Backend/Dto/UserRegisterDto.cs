using System.ComponentModel.DataAnnotations;

namespace MedHub_Backend.Dto;

public class UserRegisterDto
{
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; }

    [EmailAddress]
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }

    [Required(ErrorMessage = "ClinicId is required")]
    public int ClinicId { get; set; }
}