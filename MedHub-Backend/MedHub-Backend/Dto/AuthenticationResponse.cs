using System.ComponentModel.DataAnnotations;

namespace MedHub_Backend.Dto;

public class AuthenticationResponse
{
    [Required]
    public string Token { get; set; }

    [Required]
    public int UserId { get; set; }
}