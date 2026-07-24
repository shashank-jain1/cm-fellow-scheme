using Ardalis.Result;
using Mediator;

namespace CmScheme.Certificate.Application.Features.Certificate.ApplyForCertificate;

public sealed record ApplyForCertificateCommand(
    int ApplicantId,
    string ApplicantName,
    string ProgramName,
    DateTime StartDate,
    DateTime EndDate,
    int DurationDays,
    string CreatedBy
) : ICommand<Result<int>>;
