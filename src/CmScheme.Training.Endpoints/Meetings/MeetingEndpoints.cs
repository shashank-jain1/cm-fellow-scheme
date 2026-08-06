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

        group.MapGet("/{trainingScheduleId:int}", GetMeeting.GetById)
            .WithTags("Meetings")
            .WithName("GetMeeting")
            .WithDisplayName("Get meeting by ID");

        group.MapPut("/{trainingScheduleId:int}", UpdateMeeting.Update)
            .WithTags("Meetings")
            .WithName("UpdateMeeting")
            .WithDisplayName("Update meeting")
            .DisableAntiforgery();

        group.MapPost("/{trainingScheduleId:int}/attachments", UploadMeetingAttachment.Upload)
            .WithTags("Meetings")
            .WithName("UploadMeetingAttachment")
            .WithDisplayName("Upload meeting attachment")
            .DisableAntiforgery();

        group.MapPost("/{trainingScheduleId:int}/mom", UploadMom.Upload)
            .WithTags("Meetings")
            .WithName("UploadMom")
            .WithDisplayName("Upload minutes of meeting")
            .DisableAntiforgery();
    }
}
