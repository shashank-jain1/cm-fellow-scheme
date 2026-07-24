using Ardalis.Result;
using Mediator;

namespace CmScheme.Certificate.Application.Features.Certificate.GetCertificateStatus;

public sealed record GetCertificateStatusQuery(int CertificateId) : IQuery<Result<Core.Dtos.CertificateApplicationDto?>>;
