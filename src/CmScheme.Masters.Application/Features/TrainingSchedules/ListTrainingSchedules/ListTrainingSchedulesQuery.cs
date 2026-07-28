using Ardalis.Result;
using Mediator;

namespace CmScheme.Masters.Application.Features.TrainingSchedules.ListTrainingSchedules;

public sealed record ListTrainingSchedulesQuery : IQuery<Result<List<TrainingScheduleDto>>>
{
    public string? CalendarYear { get; init; }
    public int? ProjectId { get; init; }
    public int? DivisionId { get; init; }
}

public sealed record TrainingScheduleDto
{
    public int TrainingScheduleId { get; init; }
    public string CalendarYear { get; init; } = null!;
    public int ProjectId { get; init; }
    public string ProjectName { get; init; } = null!;
    public int? WorkId { get; init; }
    public string? WorkName { get; init; }
    public int? DivisionId { get; init; }
    public string? DivisionName { get; init; }
    public int? DistrictId { get; init; }
    public string? DistrictName { get; init; }
    public int? BlockId { get; init; }
    public string? BlockName { get; init; }
    public DateTime TrainingDate { get; init; }
    public string? VenueName { get; init; }
    public string? TrainingDescription { get; init; }
    public bool IsActive { get; init; }
}
