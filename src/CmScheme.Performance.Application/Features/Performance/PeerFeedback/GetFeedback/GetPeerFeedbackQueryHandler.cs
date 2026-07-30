using Ardalis.Result;
using CmScheme.Performance.Core.Data;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Performance.Application.Features.Performance.PeerFeedback.GetFeedback;

public sealed class GetPeerFeedbackQueryHandler(
    IPerformanceQueryDbContext performanceDbContext)
    : IQueryHandler<GetPeerFeedbackQuery, Result<List<PeerFeedbackResult>>>
{
    public async ValueTask<Result<List<PeerFeedbackResult>>> Handle(GetPeerFeedbackQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Core.Entities.PeerFeedback> query = performanceDbContext.PeerFeedbacks
            .Where(f => f.PerformanceEvaluationId == request.PerformanceEvaluationId);

        if (request.RevieweeId.HasValue)
        {
            query = query.Where(f => f.RevieweeId == request.RevieweeId.Value);
        }

        List<PeerFeedbackResult> results = await query
            .OrderByDescending(f => f.CreatedOn)
            .Select(f => new PeerFeedbackResult
            {
                PeerFeedbackId = f.PeerFeedbackId,
                PerformanceEvaluationId = f.PerformanceEvaluationId,
                ReviewerName = f.IsAnonymized ? "Anonymous" : f.ReviewerName,
                RevieweeName = f.RevieweeName,
                TechnicalSkillsRating = f.TechnicalSkillsRating,
                CommunicationRating = f.CommunicationRating,
                TeamworkRating = f.TeamworkRating,
                ProblemSolvingRating = f.ProblemSolvingRating,
                LeadershipRating = f.LeadershipRating,
                OverallRating = f.OverallRating,
                Strengths = f.Strengths,
                AreasForImprovement = f.AreasForImprovement,
                AdditionalComments = f.AdditionalComments,
                IsAnonymized = f.IsAnonymized,
                Status = f.Status,
                CreatedOn = f.CreatedOn
            })
            .ToListAsync(cancellationToken);

        return Result<List<PeerFeedbackResult>>.Success(results);
    }
}
