using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medhub_Backend.Domain.Entities;

[Table("user")]
public class User
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("username")]
    public string Username { get; set; } = null!;

    [Column("email")]
    public string Email { get; set; } = null!;

    [Column("password_hash")]
    public string? Password { get; set; }

    [Column("password_reset_code")]
    public string? PasswordResetCode { get; set; } = null!;

    [Column("has_to_reset_password")]
    public bool HasToResetPassword { get; set; } = false;

    [Column("clinic_id")]
    [ForeignKey("Clinic")]
    public int ClinicId { get; set; }

    [Column("role_id")]
    [ForeignKey("Role")]
    public int RoleId { get; set; }

    public virtual Role Role { get; set; } = null!;
    public virtual Clinic Clinic { get; set; } = null!;
    
    // used for bidirectional relationship between User and Patient
    public virtual Patient Patient { get; set; } = null!;
}