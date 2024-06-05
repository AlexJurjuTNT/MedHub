using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MedHub_Backend.Model.Enum;

namespace MedHub_Backend.Model;

[Table("patient")]
public class Patient : User
{
    [Column("cnp")]
    [Required]
    public string Cnp { get; set; }

    [Column("street")]
    public string Street { get; set; }

    [Column("city")]
    public string City { get; set; }

    [Column("date_of_birth")] public DateOnly DateOfBirth { get; set; }
    [Column("weight")] public int Weight { get; set; }
    [Column("height_cm")] public int Height { get; set; }
    [Column("gender")] public Gender Gender { get; set; }
}