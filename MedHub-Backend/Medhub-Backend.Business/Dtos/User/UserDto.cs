using System.ComponentModel.DataAnnotations;

namespace Medhub_Backend.Business.Dtos.User;

public class UserDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    public int ClinicId { get; set; }

    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string FamilyName { get; set; } = null!;
}