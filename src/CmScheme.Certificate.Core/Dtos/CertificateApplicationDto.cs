namespace CmScheme.Certificate.Core.Dtos;

public sealed class CertificateApplicationDto
{
    public int CertificateId { get; init; }
    public int ApplicantId { get; init; }
    public string ApplicantName { get; init; } = null!;
    public string ProgramName { get; init; } = null!;
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public int DurationDays { get; init; }
    public string? VerifiedBy { get; init; }
    public DateTime? CertificateIssueDate { get; init; }
    public string? CertificatePdfPath { get; init; }
    public string Status { get; init; } = null!;
    public DateTime CreatedOn { get; init; }
}
