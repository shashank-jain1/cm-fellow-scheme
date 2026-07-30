using Ardalis.Result;
using Mediator;

namespace CmScheme.Registration.Application.Features.TrainingEnrollment.EnrollTrainee;

public sealed record EnrollTraineeCommand : ICommand<Result<int>>
{
    public int TrainingScheduleId { get; init; }
    public int UserAccountId { get; init; }
}
