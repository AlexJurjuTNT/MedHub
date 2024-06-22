using System.ComponentModel.DataAnnotations;

namespace Medhub_Backend.Business.Dtos.Authentication;

public class PatientRegisterDto
{
    [EmailAddress]
    [Required]
    public string Email { get; set; }

    [Required]
    public int ClinicId { get; set; }
}