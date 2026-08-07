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
        IQueryable<CmScheme.Performance.Core.Entities.SelfAssessment> q = dbContext.SelfAssessments
            .AsNoTracking();

        if (request.UserAccountId.HasValue)
            q = q.Where(s => s.UserAccountId == request.UserAccountId.Value);
        if (request.ReviewCycleId.HasValue)
            q = q.Where(s => s.ReviewCycleId == request.ReviewCycleId);

        SelfAssessmentDto? assessment = await q
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
