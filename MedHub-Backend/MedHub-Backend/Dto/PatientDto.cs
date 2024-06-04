using System.ComponentModel.DataAnnotations;
using MedHub_Backend.Model.Enum;

namespace MedHub_Backend.Dto;

public class PatientDto
{
    [Required] public string Cnp { get; set; }
    [Required] public string Street { get; set; }
    [Required] public string City { get; set; }

    [Required] public DateOnly DateOfBirth { get; set; }
    [Required] public int Weight { get; set; }
    [Required] public int Height { get; set; }
    [Required] public Gender Gender { get; set; }
}