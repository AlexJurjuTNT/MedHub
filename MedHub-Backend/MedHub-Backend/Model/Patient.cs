using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MedHub_Backend.Model.Enum;

namespace MedHub_Backend.Model;

[Table("patient")]
public class Patient
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("cnp")]
    public string Cnp { get; set; }

    [Column("date_of_birth")]
    public DateOnly DateOfBirth { get; set; }

    [Column("weight")]
    public int Weight { get; set; }

    [Column("height_cm")]
    public int Height { get; set; }

    [Column("gender")]
    [EnumDataType(typeof(Gender))]
    public string Gender { get; set; }

    [Column("user_id")]
    [ForeignKey("User")]
    public int UserId { get; set; }

    public virtual User User { get; set; }
}