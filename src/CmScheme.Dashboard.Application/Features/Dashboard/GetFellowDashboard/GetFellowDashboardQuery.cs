using Ardalis.Result;
using Mediator;

namespace CmScheme.Dashboard.Application.Features.Dashboard.GetFellowDashboard;

public sealed record GetFellowDashboardQuery : IQuery<Result<Core.Dtos.FellowDashboardDto>>
{
    public int FellowId { get; init; }
}
