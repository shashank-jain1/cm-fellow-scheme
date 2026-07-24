using Ardalis.Result;
using Mediator;

namespace CmScheme.Certificate.Application.Features.Exit.VerifyExitCompliance;

public sealed record VerifyExitComplianceCommand(int ExitRecordId, string Status, string VerifiedBy) : ICommand<Result>;
