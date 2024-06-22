using System.ComponentModel.DataAnnotations;

namespace Medhub_Backend.Business.Dtos.Patient;

public class PatientDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Cnp { get; set; }

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