using System.ComponentModel.DataAnnotations;
using MedHub_Backend.Dto.TestResult;
using MedHub_Backend.Dto.TestType;

namespace MedHub_Backend.Dto.TestRequest;

public class TestRequestDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string RequestDate { get; set; }

    [Required]
    public int PatientId { get; set; }

    [Required]
    public int DoctorId { get; set; }

    [Required]
    public int LaboratoryId { get; set; }

    [Required]
    public List<TestTypeDto> TestTypes { get; set; }

    [Required]
    public List<TestResultDto> TestResults { get; set; }
}