using Ardalis.Result;
using Mediator;

namespace CmScheme.Dashboard.Application.Features.Dashboard.GetAdminDashboard;

public sealed record GetAdminDashboardQuery() : IQuery<Result<Core.Dtos.AdminDashboardDto>>;
