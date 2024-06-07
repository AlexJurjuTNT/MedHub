using System.ComponentModel.DataAnnotations;

namespace MedHub_Backend.Dto;

public class AddTestTypeDto
{
    [Required]
    public string Name { get; set; }
}