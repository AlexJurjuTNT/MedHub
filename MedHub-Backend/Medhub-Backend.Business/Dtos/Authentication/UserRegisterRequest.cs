using System.ComponentModel.DataAnnotations;

namespace Medhub_Backend.Business.Dtos.Authentication;

public class UserRegisterRequest
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

    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string FamilyName { get; set; } = null!;
}