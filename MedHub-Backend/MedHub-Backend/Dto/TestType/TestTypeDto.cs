using System.ComponentModel.DataAnnotations;

namespace MedHub_Backend.Dto.TestType;

public class TestTypeDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
}