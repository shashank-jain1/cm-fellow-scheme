using Ardalis.Result;
using Mediator;

namespace CmScheme.Certificate.Application.Features.Certificates.VerifyCertificate;

public sealed record VerifyCertificateQuery : IQuery<Result<CertificateVerificationResult>>
{
    public string CertificateNumber { get; init; } = null!;
}

public sealed record CertificateVerificationResult
{
    public bool IsValid { get; init; }
    public string CertificateNumber { get; init; } = null!;
    public string FellowName { get; init; } = null!;
    public string ProgramName { get; init; } = null!;
    public DateTime IssueDate { get; init; }
    public string? QrCodeUrl { get; init; }
}
