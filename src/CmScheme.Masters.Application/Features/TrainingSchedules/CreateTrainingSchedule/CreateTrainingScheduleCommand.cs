using Ardalis.Result;
using Mediator;

namespace CmScheme.Masters.Application.Features.TrainingSchedules.CreateTrainingSchedule;

public sealed record CreateTrainingScheduleCommand : ICommand<Result<int>>
{
    public string CalendarYear { get; init; } = null!;
    public int ProjectId { get; init; }
    public int? WorkId { get; init; }
    public int? DivisionId { get; init; }
    public int? DistrictId { get; init; }
    public int? BlockId { get; init; }
    public DateTime TrainingDate { get; init; }
    public string? VenueName { get; init; }
    public string? TrainingDescription { get; init; }
}
