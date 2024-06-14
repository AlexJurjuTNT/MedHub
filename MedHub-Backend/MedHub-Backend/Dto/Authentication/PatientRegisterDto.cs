using System.ComponentModel.DataAnnotations;

namespace MedHub_Backend.Dto.Authentication;

public class PatientRegisterDto
{
    [EmailAddress]
    [Required]
    public string Email { get; set; }

    [Required]
    public int ClinicId { get; set; }
}