using System.ComponentModel.DataAnnotations;

namespace Medhub_Backend.Application.Dtos.Authentication;

public class LoginRequest
{
    [Required]
    public string Username { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}