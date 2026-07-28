using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Masters.Core.Data;
using CmScheme.Masters.Core.Entities;

namespace CmScheme.Masters.Application.Features.Works.UpdateWork;

public sealed class UpdateWorkCommandHandler(IMastersCommandDbContext dbContext)
    : ICommandHandler<UpdateWorkCommand, Result>
{
    public async ValueTask<Result> Handle(
        UpdateWorkCommand request,
        CancellationToken cancellationToken)
    {
        Work? work = await dbContext.Works
            .FirstOrDefaultAsync(w => w.WorkId == request.WorkId, cancellationToken);

        if (work is null)
        {
            return Result.NotFound("Work not found.");
        }

        work.ProjectId = request.ProjectId;
        work.WorkName = request.WorkName;
        work.WorkDescription = request.WorkDescription;
        work.Priority = request.Priority;
        work.StartDate = request.StartDate;
        work.EndDate = request.EndDate;
        work.AssignedTo = request.AssignedTo;
        work.Remarks = request.Remarks;
        work.ModifiedOn = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.NoContent();
    }
}
