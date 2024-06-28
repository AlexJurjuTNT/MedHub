using System.ComponentModel.DataAnnotations;

namespace Medhub_Backend.Application.Dtos.Clinic;

public class UpdateClinicDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Location { get; set; } = null!;

    [Required]
    public string SendgridApiKey { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
}