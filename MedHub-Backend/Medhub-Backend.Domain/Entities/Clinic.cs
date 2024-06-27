using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medhub_Backend.Domain.Entities;

[Table("clinic")]
public class Clinic
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = null!;

    [Column("location")]
    public string Location { get; set; } = null!;

    [Column("sendgrid_api_key")]
    public string SendgridApiKey { get; set; } = null!;

    [Column("email")]
    public string Email { get; set; } = null!;

    public virtual ICollection<Laboratory> Laboratories { get; set; } = null!;
    public virtual ICollection<User> Users { get; set; } = null!;
}