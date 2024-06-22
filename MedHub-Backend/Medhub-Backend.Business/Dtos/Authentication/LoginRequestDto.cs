using System.ComponentModel.DataAnnotations;

namespace Medhub_Backend.Business.Dtos.Authentication;

public class LoginRequestDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}