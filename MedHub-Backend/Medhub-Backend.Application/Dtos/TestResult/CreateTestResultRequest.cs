using System.ComponentModel.DataAnnotations;

namespace Medhub_Backend.Application.Dtos.TestResult;

public class CreateTestResultRequest
{
    [Required]
    public int TestRequestId { get; set; }

    [Required]
    public List<int> TestTypesIds { get; set; } = null!;
}