using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameworkCore.EncryptColumn.Attribute;
using Medhub_Backend.Domain.Enum;

namespace Medhub_Backend.Domain.Entities;

[Table("patient")]
public class Patient
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [EncryptColumn]
    [Column("cnp")]
    public string Cnp { get; set; } = null!;

    [EncryptColumn]
    [Column("date_of_birth")]
    public DateOnly DateOfBirth { get; set; }

    [EncryptColumn]
    [Column("weight")]
    public int Weight { get; set; }

    [EncryptColumn]
    [Column("height_cm")]
    public int Height { get; set; }

    [EncryptColumn]
    [Column("gender")]
    [EnumDataType(typeof(Gender))]
    public string Gender { get; set; } = null!;

    [Column("user_id")]
    [ForeignKey("User")]
    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}