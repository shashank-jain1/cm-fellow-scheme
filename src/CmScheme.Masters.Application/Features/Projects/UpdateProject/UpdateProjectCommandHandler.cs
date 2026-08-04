using Ardalis.Result;
using CmScheme.Masters.Core.Data;
using CmScheme.Masters.Core.Entities;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Masters.Application.Features.Projects.UpdateProject;

public sealed class UpdateProjectCommandHandler(IMastersCommandDbContext dbContext)
    : ICommandHandler<UpdateProjectCommand, Result>
{
    public async ValueTask<Result> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        Project? project = await dbContext.Projects
            .FirstOrDefaultAsync(p => p.ProjectId == request.ProjectId, cancellationToken);

        if (project is null)
        {
            return Result.NotFound("Project not found.");
        }

        project.ProjectName = request.ProjectName;
        project.ProjectCode = request.ProjectCode;
        project.ProjectDescription = request.ProjectDescription;
        project.DepartmentName = request.DepartmentName;
        project.StartDate = request.StartDate;
        project.EndDate = request.EndDate;
        project.ProjectIncharge = request.ProjectIncharge;
        project.BudgetAmount = request.BudgetAmount;
        project.BudgetApprovedBy = request.BudgetApprovedBy;
        project.ProjectDocumentPath = request.ProjectDocumentPath;
        project.IsActive = request.IsActive;
        project.ModifiedOn = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.NoContent();
    }
}
