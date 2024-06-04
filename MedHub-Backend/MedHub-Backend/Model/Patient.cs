using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MedHub_Backend.Model.Enum;

namespace MedHub_Backend.Model;

[Table("patient")]
public class Patient : User
{
    [Column("cnp")]
    [MaxLength(13)]
    [MinLength(13, ErrorMessage = "CNP must be exactly 13 characters long")]
    [Required(ErrorMessage = "CNP is required")]
    public string Cnp { get; set; }

    [Column("street")]
    [Required(ErrorMessage = "Street is required")]
    [MaxLength(200, ErrorMessage = "Street name can't exceed 200 characters")]
    public string Street { get; set; }

    [Column("city")]
    [Required(ErrorMessage = "City is required")]
    [MaxLength(100, ErrorMessage = "City name can't exceed 100 characters")]
    public string City { get; set; }

    [Column("date_of_birth")] public DateOnly DateOfBirth { get; set; }
    [Column("weight")] public int Weight { get; set; }
    [Column("height_cm")] public int Height { get; set; }
    [Column("gender")] public Gender Gender { get; set; }
}