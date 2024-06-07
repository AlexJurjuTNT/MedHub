using System.ComponentModel.DataAnnotations;

namespace MedHub_Backend.Dto;

public class ResetPasswordRequestDto
{
    [Required]
    [EmailAddress]
    public string EmailAddress { get; set; } = string.Empty;

    [Required]
    public string PasswordResetCode { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    [Required, Compare("Password")]
    public string ConfirmPassword { get; set; } = string.Empty;
}