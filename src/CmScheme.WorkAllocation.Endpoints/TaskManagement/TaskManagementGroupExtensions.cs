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

        group.MapPut("/{workAllocationId:int}/progress", UpdateProgress.Handle).DisableAntiforgery();
        group.MapPut("/{workAllocationId:int}/verify", VerifyTask.Handle).DisableAntiforgery();
        group.MapGet("/{workAllocationId:int}/attachments", GetAttachments.Handle);
        group.MapPost("/{workAllocationId:int}/attachments", UploadAttachment.Handle).DisableAntiforgery();
        group.MapPost("/check-overdue", CheckOverdue.Handle).DisableAntiforgery();

        return group;
    }
}
