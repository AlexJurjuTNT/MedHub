using System.ComponentModel.DataAnnotations;

namespace Medhub_Backend.Business.Dtos.User;

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