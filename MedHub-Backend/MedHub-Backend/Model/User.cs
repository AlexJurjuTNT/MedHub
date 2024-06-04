using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedHub_Backend.Model;

[Table("user")]
public class User
{
    [Key] [Column("id")] public int Id { get; set; }

    [Required(ErrorMessage = "Username is required")]
    [MaxLength(100, ErrorMessage = "Username can't exceed 100 characters")]
    public string Username { get; set; }

    [Column("email")]
    [MaxLength(100, ErrorMessage = "Email can't exceed 100 characters")]
    [EmailAddress(ErrorMessage = "Invalid email address format")]
    public string Email { get; set; }

    [Column("password_hash")]
    [Required(ErrorMessage = "Password is required")]
    public string PasswordHash { get; set; }

    [Column("clinic_id")]
    [ForeignKey("Clinic")]
    public int ClinicId { get; set; }
    public virtual Clinic Clinic { get; set; }

    [Column("role_id")]
    [ForeignKey("Role")]
    public int RoleId { get; set; }
    public virtual Role Role { get; set; }
}