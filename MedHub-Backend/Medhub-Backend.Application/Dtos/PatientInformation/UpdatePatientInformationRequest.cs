using System.ComponentModel.DataAnnotations;

namespace Medhub_Backend.Application.Dtos.PatientInformation;

public class UpdatePatientInformationRequest
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
}