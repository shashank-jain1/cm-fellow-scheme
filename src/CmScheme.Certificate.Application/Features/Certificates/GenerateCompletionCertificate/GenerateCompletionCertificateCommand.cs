using Ardalis.Result;
using Mediator;

namespace CmScheme.Certificate.Application.Features.Certificates.GenerateCompletionCertificate;

public sealed record GenerateCompletionCertificateCommand : ICommand<Result<string>>
{
    public int ApplicantId { get; init; }
}
