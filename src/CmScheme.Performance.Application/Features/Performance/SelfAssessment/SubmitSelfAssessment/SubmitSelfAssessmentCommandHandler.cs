using Ardalis.Result;
using CmScheme.Performance.Core.Data;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Performance.Application.Features.Performance.SelfAssessment.SubmitSelfAssessment;

public sealed class SubmitSelfAssessmentCommandHandler(
    IPerformanceCommandDbContext dbContext)
    : ICommandHandler<SubmitSelfAssessmentCommand, Result>
{
    public async ValueTask<Result> Handle(
        SubmitSelfAssessmentCommand request,
        CancellationToken cancellationToken)
    {
        Core.Entities.SelfAssessment? existing = await dbContext.SelfAssessments
            .FirstOrDefaultAsync(
                s => s.UserAccountId == request.UserAccountId
                    && s.ReviewCycleId == request.ReviewCycleId
                    && s.Status != "Archived",
                cancellationToken);

        if (existing is not null)
        {
            existing.Strengths = request.Strengths;
            existing.Improvements = request.Improvements;
            existing.GoalsAchieved = request.GoalsAchieved;
            existing.GoalsMissed = request.GoalsMissed;
            existing.TrainingFeedback = request.TrainingFeedback;
            existing.OverallRating = request.OverallRating;
            existing.SubmittedOn = DateTime.UtcNow;
            existing.Status = "Submitted";
        }
        else
        {
            Core.Entities.SelfAssessment assessment = new()
            {
                UserAccountId = request.UserAccountId,
                ReviewCycleId = request.ReviewCycleId,
                Strengths = request.Strengths,
                Improvements = request.Improvements,
                GoalsAchieved = request.GoalsAchieved,
                GoalsMissed = request.GoalsMissed,
                TrainingFeedback = request.TrainingFeedback,
                OverallRating = request.OverallRating,
                SubmittedOn = DateTime.UtcNow,
                Status = "Submitted"
            };
            dbContext.SelfAssessments.Add(assessment);
        }

        await dbContext.SaveChangesAsync(cancellationToken);
        return Result.NoContent();
    }
}
