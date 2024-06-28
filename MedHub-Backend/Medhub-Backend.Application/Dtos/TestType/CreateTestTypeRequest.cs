using System.ComponentModel.DataAnnotations;

namespace Medhub_Backend.Application.Dtos.TestType;

public class CreateTestTypeRequest
{
    [Required]
    public string Name { get; set; }
}