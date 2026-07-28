using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using CmScheme.Endpoints.Abstractions;

namespace CmScheme.Training.Endpoints.Meetings;

public sealed class MeetingEndpoints : IApiEndpoint
{
    public void Configure(IEndpointRouteBuilder builder)
    {
        IEndpointRouteBuilder group = builder.MapMeetingEndpoints();

        group.MapGet("", ListMeetings.List)
            .WithTags("Meetings")
            .WithName("ListMeetings")
            .WithDisplayName("List meetings");

        group.MapPost("", CreateMeeting.Create)
            .WithTags("Meetings")
            .WithName("CreateMeeting")
            .WithDisplayName("Create a meeting");
    }
}
