using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Certificate.Core.Data;
using CmScheme.Certificate.Core.Entities;

namespace CmScheme.Certificate.Application.Features.Exit.CloseAndArchiveRecord;

public sealed class CloseAndArchiveRecordCommandHandler(ICertificateCommandDbContext dbContext)
    : ICommandHandler<CloseAndArchiveRecordCommand, Result>
{
    public async ValueTask<Result> Handle(
        CloseAndArchiveRecordCommand request,
        CancellationToken cancellationToken)
    {
        ExitRecord? exitRecord = await dbContext.ExitRecords
            .FirstOrDefaultAsync(e => e.ExitRecordId == request.ExitRecordId, cancellationToken);

        if (exitRecord is null)
        {
            return Result.NotFound("Exit record not found.");
        }

        exitRecord.CompletionStatus = "Completed";
        exitRecord.IsArchived = true;
        exitRecord.Status = "Closed";
        exitRecord.ModifiedOn = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.NoContent();
    }
}
