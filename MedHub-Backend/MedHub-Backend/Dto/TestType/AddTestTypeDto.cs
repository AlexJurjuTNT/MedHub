using System.ComponentModel.DataAnnotations;

namespace MedHub_Backend.Dto.TestType;

public class AddTestTypeDto
{
    [Required]
    public string Name { get; set; }
}