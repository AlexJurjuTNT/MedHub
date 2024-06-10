using System.ComponentModel.DataAnnotations;

namespace MedHub_Backend.Dto;

public class PatientRegisterDto
{
    [Required]
    public string Username { get; set; }

    [EmailAddress]
    [Required]
    public string Email { get; set; }

    [Required]
    public int ClinicId { get; set; }
}