using Ardalis.Result;
using Mediator;

namespace CmScheme.Certificate.Application.Features.Certificate.GenerateCertificate;

public sealed record GenerateCertificateCommand : ICommand<Result<string>>
{
    public int CertificateId { get; init; }
}
