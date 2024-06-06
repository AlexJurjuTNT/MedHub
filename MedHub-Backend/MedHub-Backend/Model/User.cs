using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameworkCore.EncryptColumn.Attribute;

namespace MedHub_Backend.Model;

[Table("user")]
public class User
{
    [Key] [Column("id")] 
    public int Id { get; set; }
    
    [EncryptColumn]
    [Column("username")] 
    public string Username { get; set; }
    
    [EncryptColumn]
    [Column("email")] 
    public string Email { get; set; }
    
    [Column("password_hash")]
    public string Password { get; set; }
    
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