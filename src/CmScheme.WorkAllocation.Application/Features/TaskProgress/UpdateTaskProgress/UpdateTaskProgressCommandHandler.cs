using Ardalis.Result;
using CmScheme.Common.Core.Services;
using CmScheme.WorkAllocation.Core.Data;
using CmScheme.WorkAllocation.Core.Entities;
using Mediator;
using Microsoft.EntityFrameworkCore;
using TaskProgressEntity = CmScheme.WorkAllocation.Core.Entities.TaskProgress;
using TaskVerificationEntity = CmScheme.WorkAllocation.Core.Entities.TaskVerification;

namespace CmScheme.WorkAllocation.Application.Features.TaskProgress.UpdateTaskProgress;

public sealed class UpdateTaskProgressCommandHandler(
    IWorkAllocationCommandDbContext dbContext,
    ICurrentUserService currentUserService,
    INotificationService notificationService)
    : ICommandHandler<UpdateTaskProgressCommand, Result<int>>
{
    public async ValueTask<Result<int>> Handle(UpdateTaskProgressCommand request, CancellationToken cancellationToken)
    {
        Core.Entities.WorkAllocation? workAllocation = await dbContext.WorkAllocations
            .FirstOrDefaultAsync(w => w.WorkAllocationId == request.WorkAllocationId, cancellationToken);

        if (workAllocation is null)
        {
            return Result<int>.NotFound("Work allocation not found.");
        }

        int? currentUserId = currentUserService.UserAccountId;
        if (currentUserId is null || workAllocation.AssignedToUserId != currentUserId)
        {
            return Result<int>.Unauthorized("You are not assigned to this work allocation.");
        }

        TaskProgressEntity taskProgress = new TaskProgressEntity
        {
            WorkAllocationId = request.WorkAllocationId,
            UserAccountId = currentUserId,
            ProgressNotes = request.ProgressNotes,
            ProgressPercentage = request.ProgressPercentage,
            FellowProgressStatus = request.Status,
            CreatedOn = DateTime.UtcNow,
            ProjectName = workAllocation.WorkProjectId,
            WorkProject = workAllocation.WorkProjectId,
            WorkDescription = workAllocation.WorkDescription,
            Priority = workAllocation.Priority,
            NumberOfSurveys = workAllocation.SurveysPerIntern,
            CompletedSurveys = 0,
            WorkStatus = request.Status
        };

        dbContext.TaskProgresses.Add(taskProgress);
        await dbContext.SaveChangesAsync(cancellationToken);

        if (request.ProgressPercentage == 100 && request.Status == "Completed")
        {
            TaskVerificationEntity taskVerification = new TaskVerificationEntity
            {
                WorkAllocationId = request.WorkAllocationId,
                VerifiedBy = 0,
                VerificationStatus = "Pending",
                Comments = null,
                VerifiedOn = null
            };

            dbContext.TaskVerifications.Add(taskVerification);
            await dbContext.SaveChangesAsync(cancellationToken);

            if (workAllocation.AssignedToUserId is not null)
            {
                await notificationService.SendEmailAsync(
                    workAllocation.CreatedBy,
                    "Task Completed - Verification Required",
                    $"A task (Work Allocation #{request.WorkAllocationId}) has been marked as completed and requires your verification.",
                    cancellationToken);
            }
        }

        return Result<int>.Success(taskProgress.TaskProgressId);
    }
}
