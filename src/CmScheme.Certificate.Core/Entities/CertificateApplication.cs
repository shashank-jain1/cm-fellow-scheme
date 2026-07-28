namespace CmScheme.Certificate.Core.Entities;

public class CertificateApplication
{
    public int CertificateId { get; set; }
    public int ApplicantId { get; set; }
    public string ApplicantName { get; set; } = null!;
    public string ProgramName { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int DurationDays { get; set; }
    public string? VerifiedBy { get; set; }
    public string? DigitalSignaturePath { get; set; }
    public DateTime? CertificateIssueDate { get; set; }
    public string? CertificatePdfPath { get; set; }
    public string Status { get; set; } = null!;
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
    public int? ModifiedBy { get; set; }
}
