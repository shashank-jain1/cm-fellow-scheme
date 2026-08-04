using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using CmScheme.Endpoints.Abstractions.Authorization;

namespace CmScheme.Training.Endpoints.Meetings;

public static class MeetingGroupExtensions
{
    public static IEndpointRouteBuilder MapMeetingEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("training/meetings")
            .WithTags("Meetings")
            .RequireAuthorization()
            .RequireModule(ModuleCodes.Training, "Read", requireScope: false);

        return group;
    }
}
