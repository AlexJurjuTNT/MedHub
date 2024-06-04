using System.ComponentModel.DataAnnotations;
using MedHub_Backend.Model.Enum;

namespace MedHub_Backend.Dto;

public class RegisterPatientDto
{
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string PasswordHash { get; set; }

    [Required(ErrorMessage = "The id of the clinic cannot be null")]
    public int ClinicId { get; set; }

    [MaxLength(13)] public string Cnp { get; set; }

    [Required(ErrorMessage = "Street is required")]
    public string Street { get; set; }

    [Required(ErrorMessage = "City is required")]
    public string City { get; set; }

    [Required] public DateOnly DateOfBirth { get; set; }
    [Required] public int Weight { get; set; }
    [Required] public int Height { get; set; }
    [Required] public Gender Gender { get; set; }
}