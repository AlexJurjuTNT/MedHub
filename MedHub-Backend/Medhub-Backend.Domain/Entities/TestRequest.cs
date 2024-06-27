using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medhub_Backend.Domain.Entities;

[Table("test_request")]
public class TestRequest
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("request_date")]
    public DateTime RequestDate { get; set; }

    [Column("patient_id1")]
    [ForeignKey("Patient")]
    public int PatientId { get; set; }

    [Column("doctor_id")]
    [ForeignKey("Doctor")]
    public int DoctorId { get; set; }

    [Column("laboratory_id")]
    [ForeignKey("Laboratory")]
    public int LaboratoryId { get; set; }

    [Column("clinic_id")]
    [ForeignKey("Clinic")]
    public int ClinicId { get; set; }

    public virtual Clinic Clinic { get; set; } = null!;
    public virtual User Patient { get; set; } = null!;
    public virtual User Doctor { get; set; } = null!;
    public virtual Laboratory Laboratory { get; set; } = null!;
    public virtual ICollection<TestResult> TestResults { get; set; } = null!;
    public virtual ICollection<TestType> TestTypes { get; set; } = null!;
}