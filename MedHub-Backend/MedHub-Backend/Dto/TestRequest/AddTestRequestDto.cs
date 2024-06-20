using System.ComponentModel.DataAnnotations;

namespace MedHub_Backend.Dto.TestRequest;

public class AddTestRequestDto
{
    [Required]
    public int PatientId { get; set; }

    [Required]
    public int DoctorId { get; set; }

    [Required]
    public List<int> TestTypesId { get; set; }

    [Required]
    public int LaboratoryId { get; set; }
}