using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MedHub_Backend.Dto;

namespace MedHub_Backend.Model;

[Table("user")]
public class User
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("username")]
    public string Username { get; set; }

    [Column("email")]
    public string Email { get; set; }

    [Column("password_hash")]
    public string? Password { get; set; }

    [Column("password_reset_code")]
    public string? PasswordResetCode { get; set; } = string.Empty;

    [Column("clinic_id")]
    [ForeignKey("Clinic")]
    public int ClinicId { get; set; }
    public virtual Clinic Clinic { get; set; }

    [Column("role_id")]
    [ForeignKey("Role")]
    public int RoleId { get; set; }
    public virtual Role Role { get; set; }


    // used for bidirectional relationship between User and Patient
    public virtual Patient Patient { get; set; }
}