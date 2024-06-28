using System.ComponentModel.DataAnnotations;
using Medhub_Backend.Application.Dtos.Laboratory;

namespace Medhub_Backend.Application.Dtos.Clinic;

public class ClinicDto
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

    [Required]
    public List<LaboratoryDto> Laboratories { get; set; } = null!;
}