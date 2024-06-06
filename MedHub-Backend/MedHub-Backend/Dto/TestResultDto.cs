using System.ComponentModel.DataAnnotations;

namespace MedHub_Backend.Dto;

public class TestResultDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public DateTime CompletionDate { get; set; }

    [Required]
    public string FilePath { get; set; }

    [Required]
    public int RequestId { get; set; }
}