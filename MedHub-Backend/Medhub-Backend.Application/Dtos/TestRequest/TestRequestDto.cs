using System.ComponentModel.DataAnnotations;
using Medhub_Backend.Application.Dtos.Laboratory;
using Medhub_Backend.Application.Dtos.TestResult;
using Medhub_Backend.Application.Dtos.TestType;
using Medhub_Backend.Application.Dtos.User;

namespace Medhub_Backend.Application.Dtos.TestRequest;

public class TestRequestDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string RequestDate { get; set; } = null!;

    [Required]
    public string UpdateTime { get; set; } = null!;

    [Required]
    public int PatientId { get; set; }

    [Required]
    public UserDto Doctor { get; set; } = null!;

    [Required]
    public LaboratoryDto Laboratory { get; set; } = null!;

    [Required]
    public List<TestTypeDto> TestTypes { get; set; } = null!;

    [Required]
    public List<TestResultDto> TestResults { get; set; } = null!;
}