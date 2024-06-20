using System.ComponentModel.DataAnnotations;

namespace MedHub_Backend.Dto.Laboratory;

public class AddLaboratoryDto
{
    [Required]
    public string Location { get; set; }

    [Required]
    public int ClinicId { get; set; }

    [Required]
    public List<int> TestTypesId { get; set; }
}