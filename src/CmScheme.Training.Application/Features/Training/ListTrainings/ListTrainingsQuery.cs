using Ardalis.Result;
using Mediator;

namespace CmScheme.Training.Application.Features.Training.ListTrainings;

public sealed record ListTrainingsQuery : IQuery<Result<List<CmScheme.Training.Core.Dtos.TrainingScheduleDto>>>
{
}
