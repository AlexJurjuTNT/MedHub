using System.ComponentModel.DataAnnotations;

namespace Medhub_Backend.Application.Dtos.Patient;

public class CreatePatientInformationRequest
{
    [Required]
    public string Cnp { get; set; } = null!;

    [Required]
    public DateOnly DateOfBirth { get; set; }

    [Required]
    public int Weight { get; set; }

    [Required]
    public int Height { get; set; }

    [Required]
    public string Gender { get; set; } = null!;

    [Required]
    public int UserId { get; set; }
}