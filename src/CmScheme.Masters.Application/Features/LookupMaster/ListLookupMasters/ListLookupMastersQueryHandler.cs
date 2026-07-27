using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Masters.Core.Data;

namespace CmScheme.Masters.Application.Features.LookupMaster.ListLookupMasters;

public sealed class ListLookupMastersQueryHandler(IMastersQueryDbContext dbContext)
    : IQueryHandler<ListLookupMastersQuery, Result<IReadOnlyList<LookupMasterListItem>>>
{
    public async ValueTask<Result<IReadOnlyList<LookupMasterListItem>>> Handle(
        ListLookupMastersQuery request,
        CancellationToken cancellationToken)
    {
        IQueryable<Core.Entities.LookupMaster> query = dbContext.LookupMasters
            .Where(lm => lm.IsActive)
            .AsNoTracking();

        if (!string.IsNullOrEmpty(request.MasterType))
        {
            query = query.Where(lm => lm.MasterType == request.MasterType);
        }

        IReadOnlyList<LookupMasterListItem> result = await query
            .OrderBy(lm => lm.SortOrder)
            .ThenBy(lm => lm.Label)
            .Select(lm => new LookupMasterListItem
            {
                LookupMasterId = lm.LookupMasterId,
                MasterType = lm.MasterType,
                Label = lm.Label,
                Value = lm.Value,
                SortOrder = lm.SortOrder,
            })
            .ToListAsync(cancellationToken);

        return Result<IReadOnlyList<LookupMasterListItem>>.Success(result);
    }
}
