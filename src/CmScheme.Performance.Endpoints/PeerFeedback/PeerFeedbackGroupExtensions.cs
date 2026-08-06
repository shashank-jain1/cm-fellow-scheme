using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using CmScheme.Endpoints.Abstractions.Authorization;

namespace CmScheme.Performance.Endpoints.PeerFeedback;

public static class PeerFeedbackGroupExtensions
{
    public static IEndpointRouteBuilder MapPeerFeedbackEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("peer-feedback")
            .RequireAuthorization()
            .RequireModule(ModuleCodes.Performance, "Read", requireScope: false);

        group.MapPost("/", SubmitFeedback.Handle).DisableAntiforgery();
        group.MapGet("/evaluation/{performanceEvaluationId:int}", GetFeedback.Handle);

        return group;
    }
}
