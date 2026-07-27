using Ardalis.Result;
using Mediator;

namespace CmScheme.Certificate.Application.Features.Exit.SubmitExitReadiness;

public sealed record SubmitExitReadinessCommand : ICommand<Result<int>>
{
    public int ApplicantId { get; init; }
    public string CompletionStatus { get; init; } = null!;
    public string VerificationFlags { get; init; } = null!;
    public string? ExitReportPath { get; init; }
    public string CreatedBy { get; init; } = null!;
}
