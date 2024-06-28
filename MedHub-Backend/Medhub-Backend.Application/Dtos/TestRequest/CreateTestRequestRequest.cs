using System.ComponentModel.DataAnnotations;

namespace Medhub_Backend.Application.Dtos.TestRequest;

public class CreateTestRequestRequest
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