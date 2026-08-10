using Ardalis.Result;
using CmScheme.Masters.Core.Data;
using Mediator;
using Microsoft.EntityFrameworkCore;
using DepartmentEntity = CmScheme.Masters.Core.Entities.Department;

namespace CmScheme.Masters.Application.Features.Department.CreateDepartment;

public sealed class CreateDepartmentCommandHandler(IMastersCommandDbContext dbContext)
    : ICommandHandler<CreateDepartmentCommand, Result<int>>
{
    public async ValueTask<Result<int>> Handle(
        CreateDepartmentCommand request,
        CancellationToken cancellationToken)
    {
        bool nameTaken = await dbContext.Departments
            .AnyAsync(d => d.DepartmentName == request.DepartmentName, cancellationToken);

        if (nameTaken)
        {
            return Result<int>.Conflict("A department with this name already exists.");
        }

        if (!string.IsNullOrWhiteSpace(request.DepartmentCode))
        {
            bool codeTaken = await dbContext.Departments
                .AnyAsync(d => d.DepartmentCode == request.DepartmentCode, cancellationToken);

            if (codeTaken)
            {
                return Result<int>.Conflict("A department with this code already exists.");
            }
        }

        DepartmentEntity entity = new DepartmentEntity
        {
            DepartmentName = request.DepartmentName,
            DepartmentCode = request.DepartmentCode,
            IsActive = true,
            CreatedOn = DateTime.UtcNow,
            ModifiedOn = DateTime.UtcNow,
        };

        dbContext.Departments.Add(entity);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(entity.DepartmentId);
    }
}
