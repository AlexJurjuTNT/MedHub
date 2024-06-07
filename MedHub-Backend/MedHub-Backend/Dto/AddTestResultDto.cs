using System.ComponentModel.DataAnnotations;

namespace MedHub_Backend.Dto;

public class AddTestResultDto
{
    [Required]
    public int TestRequestId { get; set; }
}