using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Masters.Core.Data;
using CmScheme.Masters.Core.Dtos;

namespace CmScheme.Masters.Application.Features.Location.Blocks.ListBlocksByDistrict;

public sealed class ListBlocksByDistrictQueryHandler(IMastersQueryDbContext dbContext)
    : IQueryHandler<ListBlocksByDistrictQuery, Result<List<BlockDto>>>
{
    public async ValueTask<Result<List<BlockDto>>> Handle(
        ListBlocksByDistrictQuery request,
        CancellationToken cancellationToken)
    {
        IQueryable<Core.Entities.Block> query = dbContext.Blocks
            .AsNoTracking();

        if (request.DistrictId.HasValue)
        {
            query = query.Where(b => b.DistrictId == request.DistrictId.Value);
        }

        List<BlockDto> blocks = await query
            .OrderBy(b => b.BlockName)
            .Select(b => new BlockDto(
                b.BlockId,
                b.DistrictId,
                b.BlockName,
                b.BlockCode,
                b.IsActive))
            .ToListAsync(cancellationToken);

        return Result<List<BlockDto>>.Success(blocks);
    }
}
