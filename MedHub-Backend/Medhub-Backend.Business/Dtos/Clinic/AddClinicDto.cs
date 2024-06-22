using System.ComponentModel.DataAnnotations;

namespace Medhub_Backend.Business.Dtos.Clinic;

public class AddClinicDto
{
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