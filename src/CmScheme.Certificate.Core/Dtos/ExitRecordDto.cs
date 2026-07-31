namespace CmScheme.Certificate.Core.Dtos;

public sealed class ExitRecordDto
{
    public int ExitRecordId { get; init; }
    public int ApplicantId { get; init; }
    public string CompletionStatus { get; init; } = null!;
    public string VerificationFlags { get; init; } = null!;
    public string? ExitReportPath { get; init; }
    public bool IsArchived { get; init; }
    public string Status { get; init; } = null!;
    public DateTime CreatedOn { get; init; }
}
