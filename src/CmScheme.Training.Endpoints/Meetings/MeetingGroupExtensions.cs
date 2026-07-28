using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CmScheme.Training.Endpoints.Meetings;

public static class MeetingGroupExtensions
{
    public static IEndpointRouteBuilder MapMeetingEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("training/meetings")
            .WithDisplayName("Meetings")
            .WithTags("Meetings");

        group.MapGet("/", ListMeetings.List);
        group.MapGet("/{trainingScheduleId:int}", GetMeeting.GetById);
        group.MapPost("/", CreateMeeting.Create);
        group.MapPut("/{trainingScheduleId:int}", UpdateMeeting.Update);
        group.MapPost("/{trainingScheduleId:int}/attachment", UploadMeetingAttachment.Upload);
        group.MapPost("/{trainingScheduleId:int}/mom", UploadMom.Upload);

        return builder;
    }
}
