using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedHub_Backend.Model;

[Table("test_result")]
public class TestResult
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("completion_date")]
    public DateTime CompletionDate { get; set; }

    [Column("file_path")]
    public string FilePath { get; set; }

    [Column("test_request_id")]
    [ForeignKey("TestRequest")]
    public int TestRequestId { get; set; }

    public virtual ICollection<TestType> TestTypes { get; set; }

    public virtual TestRequest TestRequest { get; set; }
}