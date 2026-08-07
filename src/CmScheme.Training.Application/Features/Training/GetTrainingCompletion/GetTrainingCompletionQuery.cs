using Ardalis.Result;
using Mediator;

namespace CmScheme.Training.Application.Features.Training.GetTrainingCompletion;

public sealed record GetTrainingCompletionQuery : IQuery<Result<Core.Dtos.TrainingCompletionDto?>>
{
    public int? TrainingScheduleId { get; init; }
    public int? UserAccountId { get; init; }
}
