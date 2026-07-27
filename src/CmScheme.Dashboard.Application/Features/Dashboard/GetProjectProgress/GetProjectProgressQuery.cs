using Ardalis.Result;
using Mediator;

namespace CmScheme.Dashboard.Application.Features.Dashboard.GetProjectProgress;

public sealed record GetProjectProgressQuery : IQuery<Result<List<Core.Dtos.ProjectProgressDto>>>
{
}
