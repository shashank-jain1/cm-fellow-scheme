using Ardalis.Result;
using CmScheme.Common.Core;
using CmScheme.Performance.Core.Data;
using CmScheme.Performance.Core.Entities;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Performance.Application.Features.Performance.SubmitPerformanceReview;

public sealed class SubmitPerformanceReviewCommandHandler(
    IPerformanceCommandDbContext dbContext)
    : ICommandHandler<SubmitPerformanceReviewCommand, Result>
{
    private static readonly Dictionary<string, (string NextLevel, string NextStatus)> Transitions = new(StringComparer.OrdinalIgnoreCase)
    {
        ["Fellow-Submit"] = (Statuses.ReviewLevel.Coordinator, Statuses.ReviewStatus.Submitted),
        ["Coordinator-Approve"] = (Statuses.ReviewLevel.Admin, Statuses.ReviewStatus.UnderReview),
        ["Coordinator-Reject"] = (Statuses.ReviewLevel.Fellow, Statuses.ReviewStatus.Rejected),
        ["Admin-Approve"] = (Statuses.ReviewLevel.Admin, Statuses.ReviewStatus.Approved),
        ["Admin-Reject"] = (Statuses.ReviewLevel.Coordinator, Statuses.ReviewStatus.Rejected),
    };

    public async ValueTask<Result> Handle(
        SubmitPerformanceReviewCommand request,
        CancellationToken cancellationToken)
    {
        PerformanceEvaluation? evaluation = await dbContext.PerformanceEvaluations
            .FirstOrDefaultAsync(p => p.PerformanceEvaluationId == request.PerformanceEvaluationId, cancellationToken);

        if (evaluation is null)
        {
            return Result.NotFound("Performance evaluation not found.");
        }

        string transitionKey = $"{evaluation.ReviewLevel}-{request.Action}";
        if (!Transitions.TryGetValue(transitionKey, out (string NextLevel, string NextStatus) transition))
        {
            return Result.Invalid(new ValidationError($"Cannot '{request.Action}' from level '{evaluation.ReviewLevel}' with status '{evaluation.ReviewStatus}'."));
        }

        string previousLevel = evaluation.ReviewLevel;
        string previousStatus = evaluation.ReviewStatus;

        evaluation.ReviewLevel = transition.NextLevel;
        evaluation.ReviewStatus = transition.NextStatus;
        evaluation.ModifiedOn = DateTime.UtcNow;

        PerformanceReviewHistory history = new()
        {
            PerformanceEvaluationId = request.PerformanceEvaluationId,
            Action = request.Action,
            PreviousLevel = previousLevel,
            NewLevel = transition.NextLevel,
            PreviousStatus = previousStatus,
            NewStatus = transition.NextStatus,
            PerformedBy = request.PerformedBy,
            Remarks = request.Remarks,
            PerformedOn = DateTime.UtcNow
        };

        dbContext.PerformanceReviewHistories.Add(history);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.NoContent();
    }
}
