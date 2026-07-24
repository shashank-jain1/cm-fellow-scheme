using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Masters.Core.Data;
using CmScheme.Masters.Core.Dtos;

namespace CmScheme.Masters.Application.Features.Location.States.ListStates;

public sealed class ListStatesQueryHandler(IMastersQueryDbContext dbContext)
    : IQueryHandler<ListStatesQuery, Result<List<StateDto>>>
{
    public async ValueTask<Result<List<StateDto>>> Handle(
        ListStatesQuery request,
        CancellationToken cancellationToken)
    {
        List<StateDto> states = await dbContext.States
            .OrderBy(s => s.DisplayOrder)
            .ThenBy(s => s.StateName)
            .Select(s => new StateDto(
                s.StateId,
                s.StateName,
                s.StateCode,
                s.StateShortName,
                s.DisplayOrder,
                s.IsActive))
            .ToListAsync(cancellationToken);

        return Result<List<StateDto>>.Success(states);
    }
}
