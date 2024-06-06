using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedHub_Backend.Model;

[Table("test_request")]
public class TestRequest
{
    [Key] [Column("id")] public int Id { get; set; }

    [Column("request_date")] public DateTime RequestDate { get; set; }

    [Column("patient_id1")]
    [ForeignKey("Patient")]
    public int PatientId { get; set; }
    public virtual User Patient { get; set; }

    [Column("doctor_id")]
    [ForeignKey("Doctor")]
    public int DoctorId { get; set; }
    public virtual User Doctor { get; set; }
    
    public virtual TestResult TestResult { get; set; }
    public virtual ICollection<TestType> TestTypes { get; set; }
}