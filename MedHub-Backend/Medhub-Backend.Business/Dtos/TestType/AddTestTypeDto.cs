using System.ComponentModel.DataAnnotations;

namespace Medhub_Backend.Business.Dtos.TestType;

public class AddTestTypeDto
{
    [Required]
    public string Name { get; set; }
}