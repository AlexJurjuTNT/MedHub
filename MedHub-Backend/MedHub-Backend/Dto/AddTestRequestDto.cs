using System.ComponentModel.DataAnnotations;

namespace MedHub_Backend.Dto;

public class AddTestRequestDto
{
    [Required]
    public int PatientId { get; set; }

    [Required]
    public int DoctorId { get; set; }

    [Required]
    public List<int> TestTypesId { get; set; }
}