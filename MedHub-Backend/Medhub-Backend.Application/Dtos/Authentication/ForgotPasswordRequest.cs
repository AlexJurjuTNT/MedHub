using System.ComponentModel.DataAnnotations;

namespace Medhub_Backend.Application.Dtos.Authentication;

public class ForgotPasswordRequest
{
    [Required]
    public string Username { get; set; } = null!;
}