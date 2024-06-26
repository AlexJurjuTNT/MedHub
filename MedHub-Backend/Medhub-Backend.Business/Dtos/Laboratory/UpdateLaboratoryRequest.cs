using System.ComponentModel.DataAnnotations;

namespace Medhub_Backend.Business.Dtos.Laboratory;

public class UpdateLaboratoryRequest
{
    [Required]
    public string Location { get; set; }

    [Required]
    public int ClinicId { get; set; }

    [Required]
    public List<int> TestTypesId { get; set; }
}