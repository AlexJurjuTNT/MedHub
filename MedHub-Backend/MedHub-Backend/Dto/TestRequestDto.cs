using System.ComponentModel.DataAnnotations;

namespace MedHub_Backend.Dto;

public class TestRequestDto
{
    [Required] public int Id { get; set; }
    [Required] public DateTime RequestDate { get; set; }
    [Required] public int PatientId { get; set; }
    [Required] public int DoctorId { get; set; }
}