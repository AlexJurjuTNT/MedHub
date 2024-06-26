using System.ComponentModel.DataAnnotations;

namespace Medhub_Backend.Business.Dtos.Authentication;

public class LoginRequest
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}