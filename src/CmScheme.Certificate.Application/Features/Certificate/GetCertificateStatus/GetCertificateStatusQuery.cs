using Ardalis.Result;
using Mediator;

namespace CmScheme.Certificate.Application.Features.Certificate.GetCertificateStatus;

public sealed record GetCertificateStatusQuery : IQuery<Result<Core.Dtos.CertificateApplicationDto?>>
{
    public int CertificateId { get; init; }
}
