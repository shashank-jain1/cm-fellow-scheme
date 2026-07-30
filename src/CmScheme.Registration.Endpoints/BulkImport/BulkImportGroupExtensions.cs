using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CmScheme.Registration.Endpoints.BulkImport;

public static class BulkImportGroupExtensions
{
    public static IEndpointRouteBuilder MapBulkImportEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("/admin/import")
            .WithTags("Admin Bulk Import")
            .RequireAuthorization("AdminPolicy");

        group.MapPost("/users", ImportUsers.Handle)
            .WithName("ImportUsers")
            .WithDisplayName("Import users from CSV")
            .DisableAntiforgery();

        return builder;
    }
}
