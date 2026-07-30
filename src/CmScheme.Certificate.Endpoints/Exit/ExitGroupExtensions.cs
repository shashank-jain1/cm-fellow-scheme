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

        group.MapPost("/readiness", SubmitReadiness.Handle);
        group.MapPut("/compliance", VerifyCompliance.Handle);
        group.MapPut("/{exitRecordId:int}/close-archive", CloseArchive.Handle);

        return builder;
    }
}
