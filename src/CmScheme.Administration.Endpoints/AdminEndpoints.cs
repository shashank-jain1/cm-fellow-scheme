using CmScheme.Common.Core.Services;
using CmScheme.Endpoints.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CmScheme.Administration.Endpoints;

public sealed class AdministrationEndpoints : IApiEndpoint
{
    public void Configure(IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("/admin")
            .WithTags("Administration")
            .RequireAuthorization("AdminPolicy");

        group.MapPost("backup", BackupDatabase)
            .WithName("BackupDatabase")
            .WithDisplayName("Trigger database backup");
    }

    private static async Task<IResult> BackupDatabase(
        IDatabaseBackupService backupService,
        CancellationToken cancellationToken)
    {
        string backupPath = await backupService.BackupAsync(cancellationToken);
        return Results.Ok(new { message = "Backup completed successfully.", path = backupPath });
    }
}
