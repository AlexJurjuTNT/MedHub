using System.ComponentModel.DataAnnotations;

namespace Medhub_Backend.Business.Dtos.User;

public class UpdateUserRequest
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    public int ClinicId { get; set; }
}