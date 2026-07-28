namespace CmScheme.Certificate.Core.Entities;

public class ExitRecord
{
    public int ExitRecordId { get; set; }
    public int ApplicantId { get; set; }
    public string CompletionStatus { get; set; } = null!;
    public string VerificationFlags { get; set; } = null!;
    public string? ExitReportPath { get; set; }
    public bool IsArchived { get; set; }
    public string Status { get; set; } = null!;
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
    public int? ModifiedBy { get; set; }
}
