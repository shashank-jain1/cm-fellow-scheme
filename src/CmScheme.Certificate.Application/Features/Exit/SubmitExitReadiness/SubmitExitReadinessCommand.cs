using Ardalis.Result;
using Mediator;

namespace CmScheme.Certificate.Application.Features.Exit.SubmitExitReadiness;

public sealed record SubmitExitReadinessCommand(
    int ApplicantId,
    string CompletionStatus,
    string VerificationFlags,
    string? ExitReportPath,
    string CreatedBy
) : ICommand<Result<int>>;
