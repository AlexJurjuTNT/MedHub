using System.ComponentModel.DataAnnotations;

namespace Medhub_Backend.Business.Dtos.TestRequest;

public class UpdateTestRequestDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string RequestDate { get; set; }

    [Required]
    public int PatientId { get; set; }

    [Required]
    public int DoctorId { get; set; }

    [Required]
    public int LaboratoryId { get; set; }

    [Required]
    public List<int> TestTypesIds { get; set; }
}