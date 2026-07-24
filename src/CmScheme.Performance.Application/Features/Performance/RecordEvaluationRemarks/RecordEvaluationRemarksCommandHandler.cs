using Ardalis.Result;
using CmScheme.Performance.Core.Data;
using CmScheme.Performance.Core.Entities;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Performance.Application.Features.Performance.RecordEvaluationRemarks;

public sealed class RecordEvaluationRemarksCommandHandler(IPerformanceCommandDbContext dbContext)
    : ICommandHandler<RecordEvaluationRemarksCommand, Result>
{
    public async ValueTask<Result> Handle(RecordEvaluationRemarksCommand request, CancellationToken cancellationToken)
    {
        PerformanceEvaluation? evaluation = await dbContext.PerformanceEvaluations
            .FirstOrDefaultAsync(p => p.PerformanceEvaluationId == request.PerformanceEvaluationId, cancellationToken);

        if (evaluation is null)
        {
            return Result.NotFound("Performance evaluation not found.");
        }

        evaluation.EvaluationRemarks = request.EvaluationRemarks;
        evaluation.EvaluatedBy = request.EvaluatedBy;
        evaluation.EvaluationDate = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
