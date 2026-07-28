using Ardalis.Result;
using Mediator;

namespace CmScheme.Training.Application.Features.Training.UpdateTrainingStatus;

public sealed record UpdateTrainingStatusCommand : ICommand<Result>
{
    public int TrainingScheduleId { get; init; }
    public string NewStatus { get; init; } = null!;
}
