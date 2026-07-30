using Ardalis.Result;
using CmScheme.Performance.Core.Data;
using Mediator;

namespace CmScheme.Performance.Application.Features.Performance.PeerFeedback.SubmitFeedback;

public sealed class SubmitPeerFeedbackCommandHandler(
    IPerformanceCommandDbContext performanceDbContext)
    : ICommandHandler<SubmitPeerFeedbackCommand, Result<int>>
{
    public async ValueTask<Result<int>> Handle(SubmitPeerFeedbackCommand request, CancellationToken cancellationToken)
    {
        decimal overallRating = (
            request.TechnicalSkillsRating +
            request.CommunicationRating +
            request.TeamworkRating +
            request.ProblemSolvingRating +
            request.LeadershipRating) / 5m;

        Core.Entities.PeerFeedback feedback = new Core.Entities.PeerFeedback
        {
            PerformanceEvaluationId = request.PerformanceEvaluationId,
            ReviewerId = request.ReviewerId,
            ReviewerName = request.ReviewerName,
            RevieweeId = request.RevieweeId,
            RevieweeName = request.RevieweeName,
            TechnicalSkillsRating = request.TechnicalSkillsRating,
            CommunicationRating = request.CommunicationRating,
            TeamworkRating = request.TeamworkRating,
            ProblemSolvingRating = request.ProblemSolvingRating,
            LeadershipRating = request.LeadershipRating,
            OverallRating = overallRating,
            Strengths = request.Strengths,
            AreasForImprovement = request.AreasForImprovement,
            AdditionalComments = request.AdditionalComments,
            IsAnonymized = request.IsAnonymized,
            Status = "Submitted",
            CreatedOn = DateTime.UtcNow
        };

        performanceDbContext.PeerFeedbacks.Add(feedback);
        await performanceDbContext.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(feedback.PeerFeedbackId);
    }
}
