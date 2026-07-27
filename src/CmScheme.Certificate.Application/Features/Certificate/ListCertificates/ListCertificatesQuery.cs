using Ardalis.Result;
using Mediator;

namespace CmScheme.Certificate.Application.Features.Certificate.ListCertificates;

public sealed record ListCertificatesQuery : IQuery<Result<List<Core.Dtos.CertificateApplicationDto>>>
{
    public int? ApplicantId { get; init; }
}
