using System.ComponentModel.DataAnnotations;

namespace MedHub_Backend.Dto.Authentication;

public class ChangeDefaultPasswordDto
{
    [Required]
    public int UserId { get; set; }
    
    [Required]
    public string Password { get; set; }
    
    [Required]
    public string ConfirmPassword { get; set; }
}