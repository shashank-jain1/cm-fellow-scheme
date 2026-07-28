using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Masters.Core.Data;
using CmScheme.Masters.Core.Entities;
using CmScheme.Masters.Application.Features.Works.CreateWork;

namespace CmScheme.Masters.Application.Features.Works.GetWorkById;

public sealed class GetWorkByIdQueryHandler(IMastersCommandDbContext dbContext)
    : IQueryHandler<GetWorkByIdQuery, Result<CreateWorkCommand>>
{
    public async ValueTask<Result<CreateWorkCommand>> Handle(
        GetWorkByIdQuery request,
        CancellationToken cancellationToken)
    {
        Work? work = await dbContext.Works
            .FirstOrDefaultAsync(w => w.WorkId == request.WorkId, cancellationToken);

        if (work is null)
        {
            return Result.NotFound("Work not found.");
        }

        return new CreateWorkCommand
        {
            ProjectId = work.ProjectId,
            WorkName = work.WorkName,
            WorkDescription = work.WorkDescription,
            Priority = work.Priority,
            StartDate = work.StartDate,
            EndDate = work.EndDate,
            AssignedTo = work.AssignedTo,
            Remarks = work.Remarks
        };
    }
}
