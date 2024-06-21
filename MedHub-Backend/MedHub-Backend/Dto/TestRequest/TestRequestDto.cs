using System.ComponentModel.DataAnnotations;
using MedHub_Backend.Dto.Laboratory;
using MedHub_Backend.Dto.TestResult;
using MedHub_Backend.Dto.TestType;
using MedHub_Backend.Dto.User;

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
    public UserDto Doctor { get; set; }

    [Required]
    public LaboratoryDto Laboratory { get; set; }

    [Required]
    public List<TestTypeDto> TestTypes { get; set; }

    [Required]
    public List<TestResultDto> TestResults { get; set; }
}