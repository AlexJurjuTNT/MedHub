using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedHub_Backend.Model;

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

    // todo: maybe this has to be hashed before inserting it into the db
    [Column("sendgrid_api_key")]
    public string SendgridApiKey { get; set; }

    public virtual ICollection<User> Users { get; set; }
}