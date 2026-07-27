using Ardalis.Result;
using Mediator;
using CmScheme.Training.Core.Dtos;

namespace CmScheme.Training.Application.Features.Meeting.ListMeetings;

public sealed record ListMeetingsQuery : IQuery<Result<List<TrainingScheduleDto>>>
{
}
