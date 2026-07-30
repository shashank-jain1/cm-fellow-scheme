using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using CmScheme.Endpoints.Abstractions.Authorization;
using CmScheme.Registration.Application.Features.ExitInterview.GetExitInterview;

namespace CmScheme.Registration.Endpoints.ExitInterview;

public static class ExitInterviewGroupExtensions
{
    public static IEndpointRouteBuilder MapExitInterviewEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("/exit-interview")
            .WithTags("Exit Interview")
            .RequireAuthorization()
            .RequireModule(ModuleCodes.Registration, "Read", requireScope: false);

        group.MapPost("/", SubmitExitInterview.Handle)
            .WithName("SubmitExitInterview")
            .WithDisplayName("Submit exit interview")
            .DisableAntiforgery()
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem();

        group.MapGet("/", GetExitInterview.Handle)
            .WithName("GetExitInterview")
            .WithDisplayName("Get exit interview")
            .Produces<ExitInterviewDto>()
            .ProducesProblem(StatusCodes.Status404NotFound);

        return builder;
    }
}
