using Ardalis.Result;
using Mediator;

namespace CmScheme.Certificate.Application.Features.Certificate.ReviewCertificate;

public sealed record ReviewCertificateCommand : ICommand<Result>
{
    public int CertificateId { get; init; }
    public string Status { get; init; } = null!;
    public string VerifiedBy { get; init; } = null!;
}
