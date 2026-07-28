using Ardalis.Result;
using CmScheme.Performance.Core.Data;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Performance.Application.Features.Performance.GetReviewHistory;

public sealed class GetReviewHistoryQueryHandler(
    IPerformanceQueryDbContext dbContext)
    : IQueryHandler<GetReviewHistoryQuery, Result<List<ReviewHistoryDto>>>
{
    public async ValueTask<Result<List<ReviewHistoryDto>>> Handle(
        GetReviewHistoryQuery request,
        CancellationToken cancellationToken)
    {
        List<ReviewHistoryDto> history = await dbContext.PerformanceReviewHistories
            .AsNoTracking()
            .Where(h => h.PerformanceEvaluationId == request.PerformanceEvaluationId)
            .OrderByDescending(h => h.PerformedOn)
            .Select(h => new ReviewHistoryDto
            {
                PerformanceReviewHistoryId = h.PerformanceReviewHistoryId,
                PerformanceEvaluationId = h.PerformanceEvaluationId,
                Action = h.Action,
                PreviousLevel = h.PreviousLevel,
                NewLevel = h.NewLevel,
                PreviousStatus = h.PreviousStatus,
                NewStatus = h.NewStatus,
                PerformedBy = h.PerformedBy,
                Remarks = h.Remarks,
                PerformedOn = h.PerformedOn
            })
            .ToListAsync(cancellationToken);

        return Result<List<ReviewHistoryDto>>.Success(history);
    }
}
