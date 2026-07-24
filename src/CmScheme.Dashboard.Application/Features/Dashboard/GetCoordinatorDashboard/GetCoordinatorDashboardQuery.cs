using Ardalis.Result;
using Mediator;

namespace CmScheme.Dashboard.Application.Features.Dashboard.GetCoordinatorDashboard;

public sealed record GetCoordinatorDashboardQuery(int CoordinatorId) : IQuery<Result<Core.Dtos.CoordinatorDashboardDto>>;
