using Ardalis.Result;
using Mediator;
using CmScheme.Masters.Core.Data;
using CmScheme.Masters.Core.Entities;

namespace CmScheme.Masters.Application.Features.Works.CreateWork;

public sealed class CreateWorkCommandHandler(IMastersCommandDbContext dbContext)
    : ICommandHandler<CreateWorkCommand, Result<int>>
{
    public async ValueTask<Result<int>> Handle(
        CreateWorkCommand request,
        CancellationToken cancellationToken)
    {
        Work work = new Work
        {
            ProjectId = request.ProjectId,
            WorkName = request.WorkName,
            WorkDescription = request.WorkDescription,
            Priority = request.Priority,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            AssignedTo = request.AssignedTo,
            Remarks = request.Remarks,
            IsActive = true,
            CreatedOn = DateTime.UtcNow,
            ModifiedOn = DateTime.UtcNow
        };

        dbContext.Works.Add(work);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(work.WorkId);
    }
}
