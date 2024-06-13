using System.ComponentModel.DataAnnotations;
using MedHub_Backend.Dto.TestType;

namespace MedHub_Backend.Dto.TestRequest;

public class TestRequestDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public DateTime RequestDate { get; set; }

    [Required]
    public int PatientId { get; set; }

    [Required]
    public int DoctorId { get; set; }

    [Required]
    public List<TestTypeDto> TestTypes { get; set; }
}