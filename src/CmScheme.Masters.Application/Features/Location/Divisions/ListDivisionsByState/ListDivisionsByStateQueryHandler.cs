using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Masters.Core.Data;
using CmScheme.Masters.Core.Dtos;

namespace CmScheme.Masters.Application.Features.Location.Divisions.ListDivisionsByState;

public sealed class ListDivisionsByStateQueryHandler(IMastersQueryDbContext dbContext)
    : IQueryHandler<ListDivisionsByStateQuery, Result<List<DivisionDto>>>
{
    public async ValueTask<Result<List<DivisionDto>>> Handle(
        ListDivisionsByStateQuery request,
        CancellationToken cancellationToken)
    {
        List<DivisionDto> divisions = await dbContext.Divisions
            .Where(d => d.StateId == request.StateId)
            .OrderBy(d => d.DivisionName)
            .Select(d => new DivisionDto(
                d.DivisionId,
                d.StateId,
                d.DivisionName,
                d.DivisionCode,
                d.IsActive))
            .ToListAsync(cancellationToken);

        return Result<List<DivisionDto>>.Success(divisions);
    }
}
