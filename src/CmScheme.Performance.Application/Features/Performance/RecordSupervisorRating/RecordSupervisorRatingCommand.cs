using Ardalis.Result;
using Mediator;

namespace CmScheme.Performance.Application.Features.Performance.RecordSupervisorRating;

public sealed record RecordSupervisorRatingCommand(int PerformanceEvaluationId, decimal SupervisorRating, string EvaluatedBy) : ICommand<Result>;
