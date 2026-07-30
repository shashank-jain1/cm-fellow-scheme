using Ardalis.Result;
using CmScheme.Dashboard.Application.Features.Dashboard.ExportDashboardToExcel;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using Microsoft.AspNetCore.Http;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Dashboard.Endpoints.Dashboards;

public static class ExportExcel
{
    public static async Task<IResult> Handle(ExportDashboardToExcelCommand command, ISender sender)
    {
        Ardalis.Result.Result<byte[]> result = await sender.Send(command);

        if (!result.IsSuccess)
        {
            return result.ToApiResult();
        }

        return Results.File(result.Value, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"dashboard_{command.Role}_{DateTime.UtcNow:yyyyMMdd}.xlsx");
    }
}
