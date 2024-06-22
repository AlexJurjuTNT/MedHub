using System.ComponentModel.DataAnnotations;
using Medhub_Backend.Business.Dtos.TestType;

namespace Medhub_Backend.Business.Dtos.Laboratory;

public class LaboratoryDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Location { get; set; }

    [Required]
    public int ClinicId { get; set; }

    [Required]
    public List<TestTypeDto> TestTypes { get; set; }
}