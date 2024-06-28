using System.ComponentModel.DataAnnotations;
using Medhub_Backend.Application.Dtos.TestType;

namespace Medhub_Backend.Application.Dtos.TestResult;

public class TestResultDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string CompletionDate { get; set; } = null!;

    [Required]
    public string FilePath { get; set; } = null!;

    [Required]
    public int TestRequestId { get; set; }

    [Required]
    public List<TestTypeDto> TestTypes { get; set; } = null!;
}