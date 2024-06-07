using System.ComponentModel.DataAnnotations;

namespace MedHub_Backend.Dto;

public class AddClinicDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Location { get; set; }

    [Required]
    public string SendgridApiKey { get; set; }
}