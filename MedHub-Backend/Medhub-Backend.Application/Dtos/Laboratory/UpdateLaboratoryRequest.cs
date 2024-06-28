using System.ComponentModel.DataAnnotations;

namespace Medhub_Backend.Application.Dtos.Laboratory;

public class UpdateLaboratoryRequest
{
    [Required]
    public string Location { get; set; } = null!;

    [Required]
    public int ClinicId { get; set; }

    [Required]
    public List<int> TestTypesId { get; set; } = null!;
}