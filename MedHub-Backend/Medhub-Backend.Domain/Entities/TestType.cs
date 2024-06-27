using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medhub_Backend.Domain.Entities;

[Table("test_type")]
public class TestType
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = null!;

    public virtual ICollection<TestRequest> TestRequests { get; set; } = null!;

    public virtual ICollection<Laboratory> Laboratories { get; set; } = null!;
}