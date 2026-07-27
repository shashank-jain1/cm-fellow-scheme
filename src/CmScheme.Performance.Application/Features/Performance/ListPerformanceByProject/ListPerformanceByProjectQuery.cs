using Ardalis.Result;
using Mediator;

namespace CmScheme.Performance.Application.Features.Performance.ListPerformanceByProject;

public sealed record ListPerformanceByProjectQuery : IQuery<Result<List<Core.Dtos.PerformanceListItemDto>>>
{
    public string ProjectName { get; init; } = null!;
}
