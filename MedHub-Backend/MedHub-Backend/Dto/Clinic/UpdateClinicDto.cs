using System.ComponentModel.DataAnnotations;

namespace MedHub_Backend.Dto.Clinic;

public class UpdateClinicDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Location { get; set; }

    [Required]
    public string SendgridApiKey { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }
}