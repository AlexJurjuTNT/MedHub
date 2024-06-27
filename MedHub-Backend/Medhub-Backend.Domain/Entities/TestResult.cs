using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medhub_Backend.Domain.Entities;

[Table("test_result")]
public class TestResult
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("completion_date")]
    public DateTime CompletionDate { get; set; }

    [Column("file_path")]
    public string FilePath { get; set; } = null!;

    [Column("test_request_id")]
    [ForeignKey("TestRequest")]
    public int TestRequestId { get; set; }

    public virtual ICollection<TestType> TestTypes { get; set; } = null!;

    public virtual TestRequest TestRequest { get; set; } = null!;
}