using System.ComponentModel.DataAnnotations;

namespace Medhub_Backend.Business.Dtos.TestResult;

public class AddTestResultDto
{
    [Required]
    public int TestRequestId { get; set; }

    [Required]
    public List<int> TestTypesIds { get; set; }
}