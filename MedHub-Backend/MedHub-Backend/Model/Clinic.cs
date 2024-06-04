using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedHub_Backend.Model;

[Table("clinic")]
public class Clinic
{
    [Key] public int Id { get; set; }

    [Column("name")]
    [MinLength(5)]
    [MaxLength(100)]
    public string Name { get; set; }

    [Column("location")]
    [MinLength(10)]
    [MaxLength(100)]
    public string Location { get; set; }

    public virtual ICollection<User> Users { get; set; }
}