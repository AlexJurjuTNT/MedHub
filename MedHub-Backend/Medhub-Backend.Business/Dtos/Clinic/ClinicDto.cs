using System.ComponentModel.DataAnnotations;
using Medhub_Backend.Business.Dtos.Laboratory;

namespace Medhub_Backend.Business.Dtos.Clinic;

public class ClinicDto
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

    [Required]
    public List<LaboratoryDto> Laboratories { get; set; }
}