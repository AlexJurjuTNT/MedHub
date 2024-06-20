using System.ComponentModel.DataAnnotations;
using MedHub_Backend.Dto.TestType;

namespace MedHub_Backend.Dto.Laboratory;

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