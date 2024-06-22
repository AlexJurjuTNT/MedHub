using System.ComponentModel.DataAnnotations;
using Medhub_Backend.Business.Dtos.TestType;

namespace Medhub_Backend.Business.Dtos.TestResult;

public class TestResultDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string CompletionDate { get; set; }

    [Required]
    public string FilePath { get; set; }

    [Required]
    public int TestRequestId { get; set; }

    [Required]
    public List<TestTypeDto> TestTypes { get; set; }
}