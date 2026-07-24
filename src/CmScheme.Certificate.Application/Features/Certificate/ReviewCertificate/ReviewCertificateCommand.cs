using Ardalis.Result;
using Mediator;

namespace CmScheme.Certificate.Application.Features.Certificate.ReviewCertificate;

public sealed record ReviewCertificateCommand(int CertificateId, string Status, string VerifiedBy) : ICommand<Result>;
