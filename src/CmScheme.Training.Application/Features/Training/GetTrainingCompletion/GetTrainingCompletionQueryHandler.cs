using Ardalis.Result;
using CmScheme.Training.Core.Data;
using CmScheme.Training.Core.Dtos;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Training.Application.Features.Training.GetTrainingCompletion;

public sealed class GetTrainingCompletionQueryHandler(
    ITrainingQueryDbContext queryDbContext)
    : IQueryHandler<GetTrainingCompletionQuery, Result<TrainingCompletionDto?>>
{
    public async ValueTask<Result<TrainingCompletionDto?>> Handle(
        GetTrainingCompletionQuery query,
        CancellationToken cancellationToken)
    {
        IQueryable<Core.Entities.TrainingCompletion> q = queryDbContext.TrainingCompletions
            .AsNoTracking();

        if (query.TrainingScheduleId.HasValue)
            q = q.Where(tc => tc.TrainingScheduleId == query.TrainingScheduleId.Value);
        if (query.UserAccountId.HasValue)
            q = q.Where(tc => tc.UserAccountId == query.UserAccountId.Value);

        TrainingCompletionDto? completion = await q
            .Select(tc => new TrainingCompletionDto(
                tc.TrainingCompletionId,
                tc.TrainingScheduleId,
                tc.UserAccountId,
                tc.Status,
                tc.CompletedOn,
                tc.CertificateIssued,
                tc.FeedbackRating,
                tc.FeedbackComments))
            .FirstOrDefaultAsync(cancellationToken);

        return Result<TrainingCompletionDto?>.Success(completion);
    }
}
