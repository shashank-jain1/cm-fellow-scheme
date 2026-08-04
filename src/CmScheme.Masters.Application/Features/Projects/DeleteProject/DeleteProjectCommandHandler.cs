using Ardalis.Result;
using CmScheme.Masters.Core.Data;
using CmScheme.Masters.Core.Entities;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Masters.Application.Features.Projects.DeleteProject;

public sealed class DeleteProjectCommandHandler(IMastersCommandDbContext dbContext)
    : ICommandHandler<DeleteProjectCommand, Result>
{
    public async ValueTask<Result> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        Project? project = await dbContext.Projects
            .FirstOrDefaultAsync(p => p.ProjectId == request.ProjectId, cancellationToken);

        if (project is null)
        {
            return Result.NotFound("Project not found.");
        }

        project.IsActive = false;
        project.ModifiedOn = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.NoContent();
    }
}
