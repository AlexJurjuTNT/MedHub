using System.ComponentModel.DataAnnotations;

namespace Medhub_Backend.Business.Dtos.TestRequest;

public class AddTestRequestDto
{
    [Required]
    public int PatientId { get; set; }

    [Required]
    public int DoctorId { get; set; }

    [Required]
    public List<int> TestTypesId { get; set; } = null!;

    [Required]
    public int LaboratoryId { get; set; }
}