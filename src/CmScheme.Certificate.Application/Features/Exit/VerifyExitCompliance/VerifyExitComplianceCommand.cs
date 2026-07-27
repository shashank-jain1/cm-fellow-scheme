using Ardalis.Result;
using Mediator;

namespace CmScheme.Certificate.Application.Features.Exit.VerifyExitCompliance;

public sealed record VerifyExitComplianceCommand : ICommand<Result>
{
    public int ExitRecordId { get; init; }
    public string Status { get; init; } = null!;
    public string VerifiedBy { get; init; } = null!;
}
