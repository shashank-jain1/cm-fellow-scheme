using Ardalis.Result;
using CmScheme.Common.Core;
using CmScheme.Training.Core.Data;
using CmScheme.Training.Core.Entities;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Training.Application.Features.Training.CompleteTraining;

public sealed class CompleteTrainingCommandHandler(
    ITrainingCommandDbContext dbContext)
    : ICommandHandler<CompleteTrainingCommand, Result<int>>
{
    public async ValueTask<Result<int>> Handle(
        CompleteTrainingCommand command,
        CancellationToken cancellationToken)
    {
        TrainingCompletion? existing = await dbContext.TrainingCompletions
            .FirstOrDefaultAsync(
                tc => tc.TrainingScheduleId == command.TrainingScheduleId
                    && tc.UserAccountId == command.UserAccountId,
                cancellationToken);

        if (existing is not null)
        {
            existing.Status = Statuses.TrainingCompletion.Completed;
            existing.CompletedOn = DateTime.UtcNow;
            existing.CertificateIssued = command.CertificateIssued;
            existing.FeedbackRating = command.FeedbackRating;
            existing.FeedbackComments = command.FeedbackComments;
            await dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success(existing.TrainingCompletionId);
        }

        TrainingCompletion completion = new()
        {
            TrainingScheduleId = command.TrainingScheduleId,
            UserAccountId = command.UserAccountId,
            Status = Statuses.TrainingCompletion.Completed,
            CompletedOn = DateTime.UtcNow,
            CertificateIssued = command.CertificateIssued,
            FeedbackRating = command.FeedbackRating,
            FeedbackComments = command.FeedbackComments,
            CreatedOn = DateTime.UtcNow
        };

        dbContext.TrainingCompletions.Add(completion);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success(completion.TrainingCompletionId);
    }
}
