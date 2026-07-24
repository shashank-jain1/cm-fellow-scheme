using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Masters.Core.Data;
using CmScheme.Masters.Core.Dtos;

namespace CmScheme.Masters.Application.Features.Works.ListWorksByProject;

public sealed class ListWorksByProjectQueryHandler(IMastersQueryDbContext dbContext)
    : IQueryHandler<ListWorksByProjectQuery, Result<List<WorkDto>>>
{
    public async ValueTask<Result<List<WorkDto>>> Handle(
        ListWorksByProjectQuery request,
        CancellationToken cancellationToken)
    {
        List<WorkDto> works = await dbContext.Works
            .Where(w => w.ProjectId == request.ProjectId)
            .OrderByDescending(w => w.CreatedOn)
            .Select(w => new WorkDto(
                w.WorkId,
                w.ProjectId,
                w.WorkName,
                w.WorkDescription,
                w.Priority,
                w.StartDate,
                w.EndDate,
                w.AssignedTo,
                w.Remarks,
                w.IsActive))
            .ToListAsync(cancellationToken);

        return Result<List<WorkDto>>.Success(works);
    }
}
