using Ardalis.Result;
using CmScheme.Dashboard.Core.Dtos;
using Mediator;

namespace CmScheme.Dashboard.Application.Features.Dashboard.GetDashboardByRole;

public sealed record GetDashboardByRoleQuery : IQuery<Result<RoleDashboardDto>>
{
    public string Role { get; init; } = null!;
}
