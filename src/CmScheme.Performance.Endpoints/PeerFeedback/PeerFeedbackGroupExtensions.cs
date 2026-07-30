using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using CmScheme.Endpoints.Abstractions.Authorization;
using CmScheme.Endpoints.Abstractions.Extensions;
using CmScheme.Performance.Application.Features.Performance.PeerFeedback.SubmitFeedback;
using CmScheme.Performance.Application.Features.Performance.PeerFeedback.GetFeedback;

namespace CmScheme.Performance.Endpoints.PeerFeedback;

public static class PeerFeedbackGroupExtensions
{
    public static IEndpointRouteBuilder MapPeerFeedbackEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("peer-feedback")
            .RequireAuthorization()
            .RequireModule(ModuleCodes.Performance, "Read", requireScope: false);

        group.MapPost("/", SubmitFeedback.Handle)
            .WithName("SubmitPeerFeedback")
            .WithDisplayName("Submit peer feedback")
            .WithTags("Peer Feedback")
            .Produces<int>()
            .ProducesValidationProblem();

        group.MapGet("/{performanceEvaluationId:int}", GetFeedback.Handle)
            .WithName("GetPeerFeedback")
            .WithDisplayName("Get peer feedback for evaluation")
            .WithTags("Peer Feedback")
            .Produces<List<PeerFeedbackResult>>();

        return builder;
    }
}
