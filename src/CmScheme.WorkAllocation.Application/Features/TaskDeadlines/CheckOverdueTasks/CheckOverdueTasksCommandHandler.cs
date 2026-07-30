using Ardalis.Result;
using CmScheme.Common.Core.Services;
using CmScheme.WorkAllocation.Core.Data;
using CmScheme.WorkAllocation.Core.Entities;
using Mediator;
using Microsoft.EntityFrameworkCore;
using TaskDeadlineEntity = CmScheme.WorkAllocation.Core.Entities.TaskDeadline;

namespace CmScheme.WorkAllocation.Application.Features.TaskDeadlines.CheckOverdueTasks;

public sealed class CheckOverdueTasksCommandHandler(
    IWorkAllocationCommandDbContext dbContext,
    INotificationService notificationService)
    : ICommandHandler<CheckOverdueTasksCommand, Result<int>>
{
    public async ValueTask<Result<int>> Handle(CheckOverdueTasksCommand request, CancellationToken cancellationToken)
    {
        DateTime now = DateTime.UtcNow;

        List<TaskDeadlineEntity> overdueDeadlines = await dbContext.TaskDeadlines
            .Where(d => !d.IsOverdue && d.DeadlineDate < now)
            .ToListAsync(cancellationToken);

        int count = 0;

        foreach (TaskDeadlineEntity deadline in overdueDeadlines)
        {
            deadline.IsOverdue = true;
            count++;

            Core.Entities.WorkAllocation? workAllocation = await dbContext.WorkAllocations
                .FirstOrDefaultAsync(w => w.WorkAllocationId == deadline.WorkAllocationId, cancellationToken);

            if (workAllocation is not null)
            {
                if (workAllocation.AssignedToUserId is not null)
                {
                    await notificationService.SendEmailAsync(
                        workAllocation.CreatedBy,
                        "Task Overdue Warning",
                        $"Task (Work Allocation #{deadline.WorkAllocationId}) is now overdue. Deadline was {deadline.DeadlineDate:dd MMM yyyy}.",
                        cancellationToken);
                }
            }
        }

        if (count > 0)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        List<TaskDeadlineEntity> reminderDeadlines = await dbContext.TaskDeadlines
            .Where(d => !d.IsOverdue
                && d.LastReminderSentOn == null
                && d.DeadlineDate <= now.AddDays(d.ReminderDaysBefore))
            .ToListAsync(cancellationToken);

        foreach (TaskDeadlineEntity deadline in reminderDeadlines)
        {
            Core.Entities.WorkAllocation? workAllocation = await dbContext.WorkAllocations
                .FirstOrDefaultAsync(w => w.WorkAllocationId == deadline.WorkAllocationId, cancellationToken);

            if (workAllocation is not null && workAllocation.AssignedToUserId is not null)
            {
                await notificationService.SendEmailAsync(
                    workAllocation.CreatedBy,
                    "Task Deadline Reminder",
                    $"Reminder: Task (Work Allocation #{deadline.WorkAllocationId}) is due on {deadline.DeadlineDate:dd MMM yyyy}.",
                    cancellationToken);

                deadline.LastReminderSentOn = now;
            }
        }

        if (reminderDeadlines.Count > 0)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        return Result<int>.Success(count);
    }
}
