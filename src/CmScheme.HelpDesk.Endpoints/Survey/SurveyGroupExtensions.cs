using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using CmScheme.Endpoints.Abstractions.Authorization;

namespace CmScheme.HelpDesk.Endpoints.Survey;

public static class SurveyGroupExtensions
{
    public static IEndpointRouteBuilder MapSurveyEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("surveys")
            .RequireAuthorization()
            .RequireModule(ModuleCodes.HelpDesk, "Read", requireScope: false);

        group.MapPost("/", SubmitSurvey.Handle);
        group.MapGet("/ticket/{ticketId:int}", GetSurvey.Handle);

        return builder;
    }
}
