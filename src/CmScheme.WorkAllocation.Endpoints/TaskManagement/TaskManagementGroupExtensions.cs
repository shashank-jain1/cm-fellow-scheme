using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using CmScheme.Endpoints.Abstractions.Authorization;

namespace CmScheme.WorkAllocation.Endpoints.TaskManagement;

public static class TaskManagementGroupExtensions
{
    public static IEndpointRouteBuilder MapTaskManagementEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("task-management")
            .RequireAuthorization()
            .RequireModule(ModuleCodes.WorkAllocation, "Read", requireScope: false);

        group.MapPut("/progress/{workAllocationId:int}", UpdateProgress.Handle);
        group.MapPost("/verify/{workAllocationId:int}", VerifyTask.Handle);
        group.MapPost("/attachments/{workAllocationId:int}", UploadAttachment.Handle);
        group.MapGet("/attachments/{workAllocationId:int}", GetAttachments.Handle);
        group.MapPost("/check-overdue", CheckOverdue.Handle);

        return builder;
    }
}
