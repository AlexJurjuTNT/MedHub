using System.ComponentModel.DataAnnotations;

namespace MedHub_Backend.Dto.TestResult;

public class AddTestResultDto
{
    [Required]
    public int TestRequestId { get; set; }

    [Required]
    public List<int> TestTypesIds { get; set; }
}