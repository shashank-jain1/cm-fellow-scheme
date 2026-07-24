using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CmScheme.Training.Endpoints.Meetings;

public static class MeetingGroupExtensions
{
    public static IEndpointRouteBuilder MapMeetingGroup(this IEndpointRouteBuilder builder)
    {
        return builder.MapGroup("training/meetings")
            .WithDisplayName("Meetings")
            .WithTags("Meetings");
    }
}
