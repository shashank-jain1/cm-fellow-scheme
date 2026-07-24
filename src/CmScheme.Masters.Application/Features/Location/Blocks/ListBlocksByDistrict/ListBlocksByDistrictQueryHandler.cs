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
        List<BlockDto> blocks = await dbContext.Blocks
            .Where(b => b.DistrictId == request.DistrictId)
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
