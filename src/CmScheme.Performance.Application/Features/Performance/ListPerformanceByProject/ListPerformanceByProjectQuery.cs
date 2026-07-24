using Ardalis.Result;
using Mediator;

namespace CmScheme.Performance.Application.Features.Performance.ListPerformanceByProject;

public sealed record ListPerformanceByProjectQuery(string ProjectName) : IQuery<Result<List<Core.Dtos.PerformanceListItemDto>>>;
