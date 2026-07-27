using Ardalis.Result;
using Mediator;

namespace CmScheme.Certificate.Application.Features.Certificate.ApplyForCertificate;

public sealed record ApplyForCertificateCommand : ICommand<Result<int>>
{
    public int ApplicantId { get; init; }
    public string ApplicantName { get; init; } = null!;
    public string ProgramName { get; init; } = null!;
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public int DurationDays { get; init; }
    public string CreatedBy { get; init; } = null!;
}
