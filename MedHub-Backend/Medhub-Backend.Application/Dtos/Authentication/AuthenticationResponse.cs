using System.ComponentModel.DataAnnotations;

namespace Medhub_Backend.Application.Dtos.Authentication;

public class AuthenticationResponse
{
    [Required]
    public string Token { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public bool HasToResetPassword { get; set; }
}