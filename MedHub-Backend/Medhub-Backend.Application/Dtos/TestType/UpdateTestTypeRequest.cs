using System.ComponentModel.DataAnnotations;

namespace Medhub_Backend.Application.Dtos.TestType;

public class UpdateTestTypeRequest
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;
}