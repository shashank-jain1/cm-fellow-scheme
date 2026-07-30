using Ardalis.Result;
using Mediator;

namespace CmScheme.WorkAllocation.Application.Features.TaskDeadlines.CheckOverdueTasks;

public sealed record CheckOverdueTasksCommand : ICommand<Result<int>>;
