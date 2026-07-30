using Ardalis.Result;
using CmScheme.Performance.Core.Data;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Performance.Application.Features.Performance.SelfAssessment.GetSelfAssessment;

public sealed class GetSelfAssessmentQueryHandler(
    IPerformanceQueryDbContext dbContext)
    : IQueryHandler<GetSelfAssessmentQuery, Result<SelfAssessmentDto>>
{
    public async ValueTask<Result<SelfAssessmentDto>> Handle(
        GetSelfAssessmentQuery request,
        CancellationToken cancellationToken)
    {
        SelfAssessmentDto? assessment = await dbContext.SelfAssessments
            .AsNoTracking()
            .Where(s => s.UserAccountId == request.UserAccountId
                && (request.ReviewCycleId == null || s.ReviewCycleId == request.ReviewCycleId))
            .OrderByDescending(s => s.SubmittedOn)
            .Select(s => new SelfAssessmentDto
            {
                SelfAssessmentId = s.SelfAssessmentId,
                UserAccountId = s.UserAccountId,
                ReviewCycleId = s.ReviewCycleId,
                Strengths = s.Strengths,
                Improvements = s.Improvements,
                GoalsAchieved = s.GoalsAchieved,
                GoalsMissed = s.GoalsMissed,
                TrainingFeedback = s.TrainingFeedback,
                OverallRating = s.OverallRating,
                SubmittedOn = s.SubmittedOn,
                Status = s.Status
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (assessment is null)
        {
            return Result.NotFound("Self-assessment not found.");
        }

        return Result<SelfAssessmentDto>.Success(assessment);
    }
}
