using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medhub_Backend.Domain.Model;

[Table("laboratory")]
public class Laboratory
{
    [Key]
    public int Id { get; set; }

    [Column("location")]
    public string Location { get; set; }

    [Column("clinic_id")]
    [ForeignKey("Clinic")]
    public int ClinicId { get; set; }

    public virtual Clinic Clinic { get; set; }

    public virtual ICollection<TestType> TestTypes { get; set; }
}