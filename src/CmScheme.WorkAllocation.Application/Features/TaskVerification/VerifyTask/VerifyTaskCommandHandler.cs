using Ardalis.Result;
using CmScheme.Common.Core.Services;
using CmScheme.WorkAllocation.Core.Data;
using CmScheme.WorkAllocation.Core.Entities;
using Mediator;
using Microsoft.EntityFrameworkCore;
using TaskVerificationEntity = CmScheme.WorkAllocation.Core.Entities.TaskVerification;

namespace CmScheme.WorkAllocation.Application.Features.TaskVerification.VerifyTask;

public sealed class VerifyTaskCommandHandler(
    IWorkAllocationCommandDbContext dbContext,
    ICurrentUserService currentUserService,
    INotificationService notificationService)
    : ICommandHandler<VerifyTaskCommand, Result>
{
    public async ValueTask<Result> Handle(VerifyTaskCommand request, CancellationToken cancellationToken)
    {
        Core.Entities.WorkAllocation? workAllocation = await dbContext.WorkAllocations
            .FirstOrDefaultAsync(w => w.WorkAllocationId == request.WorkAllocationId, cancellationToken);

        if (workAllocation is null)
        {
            return Result.NotFound("Work allocation not found.");
        }

        int? currentUserId = currentUserService.UserAccountId;
        if (currentUserId is null)
        {
            return Result.Unauthorized("User is not authenticated.");
        }

        string? userRole = currentUserService.Role;
        if (userRole != "Admin" && userRole != "Guide" && userRole != "Coordinator")
        {
            return Result.Unauthorized("Only supervisors can verify tasks.");
        }

        TaskVerificationEntity? existingVerification = await dbContext.TaskVerifications
            .FirstOrDefaultAsync(
                tv => tv.WorkAllocationId == request.WorkAllocationId && tv.VerificationStatus == "Pending",
                cancellationToken);

        if (existingVerification is not null)
        {
            existingVerification.VerificationStatus = request.VerificationStatus;
            existingVerification.Comments = request.Comments;
            existingVerification.VerifiedBy = currentUserId.Value;
            existingVerification.VerifiedOn = DateTime.UtcNow;
        }
        else
        {
            TaskVerificationEntity taskVerification = new TaskVerificationEntity
            {
                WorkAllocationId = request.WorkAllocationId,
                VerifiedBy = currentUserId.Value,
                VerificationStatus = request.VerificationStatus,
                Comments = request.Comments,
                VerifiedOn = DateTime.UtcNow
            };
            dbContext.TaskVerifications.Add(taskVerification);
        }

        if (request.VerificationStatus == "Approved")
        {
            workAllocation.Status = "Completed";
            workAllocation.ModifiedOn = DateTime.UtcNow;
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        if (workAllocation.AssignedToUserId is not null)
        {
            string emailSubject = request.VerificationStatus == "Approved"
                ? "Task Approved"
                : "Task Rejected";

            string emailBody = request.VerificationStatus == "Approved"
                ? $"Your task (Work Allocation #{request.WorkAllocationId}) has been approved."
                : $"Your task (Work Allocation #{request.WorkAllocationId}) has been rejected. Comments: {request.Comments}";

            await notificationService.SendEmailAsync(
                workAllocation.CreatedBy,
                emailSubject,
                emailBody,
                cancellationToken);
        }

        return Result.NoContent();
    }
}
