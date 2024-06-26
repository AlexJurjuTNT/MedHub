using System.ComponentModel.DataAnnotations;

namespace Medhub_Backend.Business.Dtos.Laboratory;

public class CreateLaboratoryRequest
{
    [Required]
    public string Location { get; set; }

    [Required]
    public int ClinicId { get; set; }

    [Required]
    public List<int> TestTypesId { get; set; }
}