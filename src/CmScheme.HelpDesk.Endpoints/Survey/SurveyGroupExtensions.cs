using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using CmScheme.Endpoints.Abstractions.Authorization;
using CmScheme.HelpDesk.Core.Dtos;

namespace CmScheme.HelpDesk.Endpoints.Survey;

public static class SurveyGroupExtensions
{
    public static IEndpointRouteBuilder MapSurveyEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("surveys")
            .RequireAuthorization()
            .RequireModule(ModuleCodes.HelpDesk, "Read", requireScope: false);

        group.MapPost("/", SubmitSurvey.Handle)
            .WithName("SubmitSurvey")
            .WithDisplayName("Submit survey")
            .DisableAntiforgery()
            .Produces<int>()
            .ProducesValidationProblem();

        group.MapGet("/ticket/{ticketId:int}", GetSurvey.Handle)
            .WithName("GetSurveyByTicketId")
            .WithDisplayName("Get survey by ticket ID")
            .Produces<TicketSatisfactionSurveyDto>();

        return group;
    }
}
