using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using CmScheme.Endpoints.Abstractions.Authorization;

namespace CmScheme.Certificate.Endpoints.Exit;

public static class ExitGroupExtensions
{
    public static IEndpointRouteBuilder MapExitEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("exit")
            .RequireAuthorization()
            .RequireModule(ModuleCodes.Certificate, "Read", requireScope: false);

        group.MapPost("/readiness", SubmitReadiness.Handle).DisableAntiforgery();
        group.MapPost("/compliance", VerifyCompliance.Handle).DisableAntiforgery();
        group.MapPut("/{exitRecordId:int}/close-archive", CloseArchive.Handle).DisableAntiforgery();

        return group;
    }
}
