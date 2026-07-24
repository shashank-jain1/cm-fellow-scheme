using Ardalis.Result;
using CmScheme.Performance.Core.Data;
using CmScheme.Performance.Core.Entities;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Performance.Application.Features.Performance.RecordSupervisorRating;

public sealed class RecordSupervisorRatingCommandHandler(IPerformanceCommandDbContext dbContext)
    : ICommandHandler<RecordSupervisorRatingCommand, Result>
{
    public async ValueTask<Result> Handle(RecordSupervisorRatingCommand request, CancellationToken cancellationToken)
    {
        PerformanceEvaluation? evaluation = await dbContext.PerformanceEvaluations
            .FirstOrDefaultAsync(p => p.PerformanceEvaluationId == request.PerformanceEvaluationId, cancellationToken);

        if (evaluation is null)
        {
            return Result.NotFound("Performance evaluation not found.");
        }

        evaluation.SupervisorRating = request.SupervisorRating;
        evaluation.EvaluatedBy = request.EvaluatedBy;
        evaluation.EvaluationDate = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
