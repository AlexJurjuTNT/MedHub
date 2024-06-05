using System.ComponentModel.DataAnnotations;

namespace MedHub_Backend.Dto;

public class ClinicDto
{
    [Required(ErrorMessage = "Id is required")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Location is required")]
    public string Location { get; set; }
}