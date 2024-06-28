using System.ComponentModel.DataAnnotations;

namespace Medhub_Backend.Application.Dtos.Authentication;

public class ChangeDefaultPasswordDto
{
    [Required]
    public int UserId { get; set; }

    [Required]
    public string Password { get; set; } = null!;

    [Required]
    public string ConfirmPassword { get; set; } = null!;
}