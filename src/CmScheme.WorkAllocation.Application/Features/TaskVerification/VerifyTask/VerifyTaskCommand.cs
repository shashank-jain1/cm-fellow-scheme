using Ardalis.Result;
using Mediator;

namespace CmScheme.WorkAllocation.Application.Features.TaskVerification.VerifyTask;

public sealed record VerifyTaskCommand : ICommand<Result>
{
    public int WorkAllocationId { get; init; }
    public string VerificationStatus { get; init; } = null!;
    public string? Comments { get; init; }
}
