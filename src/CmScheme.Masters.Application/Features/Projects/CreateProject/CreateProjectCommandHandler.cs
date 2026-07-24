using Ardalis.Result;
using Mediator;
using CmScheme.Masters.Core.Data;
using CmScheme.Masters.Core.Entities;

namespace CmScheme.Masters.Application.Features.Projects.CreateProject;

public sealed class CreateProjectCommandHandler(IMastersCommandDbContext dbContext)
    : ICommandHandler<CreateProjectCommand, Result<int>>
{
    public async ValueTask<Result<int>> Handle(
        CreateProjectCommand request,
        CancellationToken cancellationToken)
    {
        Project project = new Project
        {
            ProjectName = request.ProjectName,
            ProjectCode = request.ProjectCode,
            ProjectDescription = request.ProjectDescription,
            DepartmentName = request.DepartmentName,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            ProjectIncharge = request.ProjectIncharge,
            BudgetAmount = request.BudgetAmount,
            BudgetApprovedBy = request.BudgetApprovedBy,
            ProjectDocumentPath = request.ProjectDocumentPath,
            IsActive = true,
            CreatedOn = DateTime.UtcNow,
            ModifiedOn = DateTime.UtcNow
        };

        dbContext.Projects.Add(project);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(project.ProjectId);
    }
}
