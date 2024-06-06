using System.ComponentModel.DataAnnotations;
using MedHub_Backend.Model.Enum;

namespace MedHub_Backend.Dto;

public class AddPatientDataDto
{
    [Required] public int Cnp { get; set; }
    [Required] public DateOnly DateOfBirth { get; set; }
    [Required] public int Weight { get; set; }
    [Required] public int Height { get; set; }
    [Required] public Gender Gender { get; set; }
    [Required] public int UserId { get; set; }
}