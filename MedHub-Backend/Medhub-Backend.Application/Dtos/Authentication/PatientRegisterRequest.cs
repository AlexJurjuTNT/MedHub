using System.ComponentModel.DataAnnotations;

namespace Medhub_Backend.Application.Dtos.Authentication;

public class PatientRegisterRequest
{
    [EmailAddress]
    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public int ClinicId { get; set; }

    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string FamilyName { get; set; } = null!;
}