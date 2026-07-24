using Ardalis.Result;
using CmScheme.Dashboard.Application.Features.Dashboard.GetCoordinatorDashboard;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Dashboard.Endpoints.Dashboards;

public static class GetCoordinator
{
    public static async Task<IResult> Handle(int coordinatorId, ISender sender)
    {
        GetCoordinatorDashboardQuery query = new GetCoordinatorDashboardQuery(coordinatorId);
        Result<Core.Dtos.CoordinatorDashboardDto> result = await sender.Send(query);
        return result.ToApiResult();
    }
}
