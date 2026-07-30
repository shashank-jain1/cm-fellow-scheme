namespace CmScheme.Certificate.Core.Entities;

public class CertificateVerification
{
    public int CertificateVerificationId { get; set; }
    public int CertificateId { get; set; }
    public string CertificateNumber { get; set; } = null!;
    public string FellowName { get; set; } = null!;
    public string ProgramName { get; set; } = null!;
    public DateTime IssueDate { get; set; }
    public string VerificationUrl { get; set; } = null!;
    public string? QrCodeData { get; set; }
    public string Status { get; set; } = null!;
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
    public int? ModifiedBy { get; set; }
}
