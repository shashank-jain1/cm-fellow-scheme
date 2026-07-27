using Ardalis.Result;
using Mediator;

namespace CmScheme.Dashboard.Application.Features.Dashboard.GetCoordinatorDashboard;

public sealed record GetCoordinatorDashboardQuery : IQuery<Result<Core.Dtos.CoordinatorDashboardDto>>
{
    public int CoordinatorId { get; init; }
}
