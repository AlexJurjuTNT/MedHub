using System.ComponentModel.DataAnnotations;

namespace MedHub_Backend.Dto;

public class LoginRequestDto
{
    [Required] [EmailAddress] public string Email { get; set; }

    [Required] public string Password { get; set; }
}