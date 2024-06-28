using System.ComponentModel.DataAnnotations;

namespace Medhub_Backend.Application.Dtos.User;

public class UpdateUserRequest
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public string Username { get; set; } = null!;

    [Required]
    public int ClinicId { get; set; }

    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string FamilyName { get; set; } = null!;
}