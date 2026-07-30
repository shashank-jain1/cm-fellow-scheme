using Ardalis.Result;
using CmScheme.Dashboard.Application.Features.Dashboard.ExportDashboardToPdf;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using Microsoft.AspNetCore.Http;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Dashboard.Endpoints.Dashboards;

public static class ExportPdf
{
    public static async Task<IResult> Handle(ExportDashboardToPdfCommand command, ISender sender)
    {
        Ardalis.Result.Result<byte[]> result = await sender.Send(command);

        if (!result.IsSuccess)
        {
            return result.ToApiResult();
        }

        return Results.File(result.Value, "application/pdf", $"dashboard_{command.Role}_{DateTime.UtcNow:yyyyMMdd}.pdf");
    }
}
