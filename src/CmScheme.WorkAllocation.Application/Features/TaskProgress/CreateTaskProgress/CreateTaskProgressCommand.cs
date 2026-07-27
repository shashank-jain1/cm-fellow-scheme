using Ardalis.Result;
using Mediator;

namespace CmScheme.WorkAllocation.Application.Features.TaskProgress.CreateTaskProgress;

public sealed record CreateTaskProgressCommand : ICommand<Result<int>>
{
    public int WorkAllocationId { get; init; }
    public string ProjectName { get; init; } = null!;
    public string WorkProject { get; init; } = null!;
    public string WorkDescription { get; init; } = null!;
    public string Priority { get; init; } = null!;
    public int NumberOfSurveys { get; init; }
    public int CompletedSurveys { get; init; }
    public DateTime? CompletionDate { get; init; }
    public string WorkStatus { get; init; } = null!;
    public decimal CompletionPercentage { get; init; }
}
