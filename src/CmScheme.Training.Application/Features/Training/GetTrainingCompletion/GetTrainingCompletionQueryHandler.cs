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
        TrainingCompletionDto? completion = await queryDbContext.TrainingCompletions
            .AsNoTracking()
            .Where(tc => tc.TrainingScheduleId == query.TrainingScheduleId
                      && tc.UserAccountId == query.UserAccountId)
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
