using Ardalis.Result;
using CmScheme.Certificate.Core.Data;
using CmScheme.Certificate.Core.Entities;
using Mediator;

namespace CmScheme.Certificate.Application.Features.Exit.SubmitExitReadiness;

public sealed class SubmitExitReadinessCommandHandler(ICertificateCommandDbContext dbContext)
    : ICommandHandler<SubmitExitReadinessCommand, Result<int>>
{
    public async ValueTask<Result<int>> Handle(SubmitExitReadinessCommand request, CancellationToken cancellationToken)
    {
        ExitRecord exitRecord = new ExitRecord
        {
            ApplicantId = request.ApplicantId,
            CompletionStatus = request.CompletionStatus,
            VerificationFlags = request.VerificationFlags,
            ExitReportPath = request.ExitReportPath,
            IsArchived = false,
            Status = "Pending",
            CreatedOn = DateTime.UtcNow,
            CreatedBy = request.CreatedBy
        };

        dbContext.ExitRecords.Add(exitRecord);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(exitRecord.ExitRecordId);
    }
}
