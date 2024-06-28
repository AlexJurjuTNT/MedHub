using System.ComponentModel.DataAnnotations;
using Medhub_Backend.Application.Dtos.TestType;

namespace Medhub_Backend.Application.Dtos.Laboratory;

public class LaboratoryDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Location { get; set; } = null!;

    [Required]
    public int ClinicId { get; set; }

    [Required]
    public List<TestTypeDto> TestTypes { get; set; } = null!;
}