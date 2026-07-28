using Ardalis.Result;
using Mediator;

namespace CmScheme.Training.Application.Features.Training.CreateTraining;

public sealed record CreateTrainingCommand : ICommand<Result<int>>
{
    public int ProjectId { get; init; }
    public int WorkProjectId { get; init; }
    public string TrainingTitle { get; init; } = string.Empty;
    public string? TrainingCategory { get; init; }
    public string? TrainingDescription { get; init; }
    public List<string> TargetUserTypes { get; init; } = [];
    public DateTime Date { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
    public string? Mode { get; init; }
    public List<int> ApplicableDivisionIds { get; init; } = [];
    public List<int> ApplicableDistrictIds { get; init; } = [];
    public List<int> ApplicableBlockIds { get; init; } = [];
    public string? TrainerName { get; init; }
    public string? TrainerMobile { get; init; }
    public bool AttendanceRequired { get; init; }
    public string? Remarks { get; init; }
}
