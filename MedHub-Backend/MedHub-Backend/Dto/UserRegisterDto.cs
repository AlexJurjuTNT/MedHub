using System.ComponentModel.DataAnnotations;

namespace MedHub_Backend.Dto;

public class UserRegisterDto
{
    [Required]
    public string Username { get; set; }

    [EmailAddress]
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public int ClinicId { get; set; }
}