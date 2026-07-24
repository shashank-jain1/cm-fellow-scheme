using Ardalis.Result;
using Mediator;

namespace CmScheme.Performance.Application.Features.Performance.RecordEvaluationRemarks;

public sealed record RecordEvaluationRemarksCommand(int PerformanceEvaluationId, string EvaluationRemarks, string EvaluatedBy) : ICommand<Result>;
