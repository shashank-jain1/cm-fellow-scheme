using Ardalis.Result;
using Mediator;

namespace CmScheme.WorkAllocation.Application.Features.TaskProgress.CreateTaskProgress;

public sealed record CreateTaskProgressCommand(
    int WorkAllocationId,
    string ProjectName,
    string WorkProject,
    string WorkDescription,
    string Priority,
    int NumberOfSurveys,
    int CompletedSurveys,
    DateTime? CompletionDate,
    string WorkStatus,
    decimal CompletionPercentage
) : ICommand<Result<int>>;
