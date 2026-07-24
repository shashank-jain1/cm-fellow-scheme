using Ardalis.Result;
using CmScheme.Certificate.Core.Data;
using CmScheme.Certificate.Core.Entities;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Certificate.Application.Features.Exit.VerifyExitCompliance;

public sealed class VerifyExitComplianceCommandHandler(ICertificateCommandDbContext dbContext)
    : ICommandHandler<VerifyExitComplianceCommand, Result>
{
    public async ValueTask<Result> Handle(VerifyExitComplianceCommand request, CancellationToken cancellationToken)
    {
        ExitRecord? exitRecord = await dbContext.ExitRecords
            .FirstOrDefaultAsync(e => e.ExitRecordId == request.ExitRecordId, cancellationToken);

        if (exitRecord is null)
        {
            return Result.NotFound("Exit record not found.");
        }

        exitRecord.Status = request.Status;
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
