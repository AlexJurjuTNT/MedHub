using System.ComponentModel.DataAnnotations;

namespace MedHub_Backend.Dto.Patient;

public class AddPatientDataDto
{
    [Required]
    public int Cnp { get; set; }

    [Required]
    public DateOnly DateOfBirth { get; set; }

    [Required]
    public int Weight { get; set; }

    [Required]
    public int Height { get; set; }

    [Required]
    public string Gender { get; set; }

    [Required]
    public int UserId { get; set; }
}