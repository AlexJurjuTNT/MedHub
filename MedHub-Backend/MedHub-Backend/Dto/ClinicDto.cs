using System.ComponentModel.DataAnnotations;

namespace MedHub_Backend.Dto;

public class ClinicDto
{
    [Required(ErrorMessage = "Id is required")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [MinLength(5)]
    [MaxLength(100)]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Location is required")]
    [MinLength(10)]
    [MaxLength(100)]
    public string Location { get; set; }
}