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
    public string Name { get; set; }

    [Column("location")]
    public string Location { get; set; }

    [Column("sendgrid_api_key")]
    public string SendgridApiKey { get; set; }

    [Column("email")]
    public string Email { get; set; }

    public virtual ICollection<Laboratory> Laboratories { get; set; }
    public virtual ICollection<User> Users { get; set; }
}