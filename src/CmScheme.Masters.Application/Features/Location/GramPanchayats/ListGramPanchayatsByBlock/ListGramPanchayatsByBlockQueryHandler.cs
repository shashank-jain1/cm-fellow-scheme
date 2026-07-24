using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Masters.Core.Data;
using CmScheme.Masters.Core.Dtos;

namespace CmScheme.Masters.Application.Features.Location.GramPanchayats.ListGramPanchayatsByBlock;

public sealed class ListGramPanchayatsByBlockQueryHandler(IMastersQueryDbContext dbContext)
    : IQueryHandler<ListGramPanchayatsByBlockQuery, Result<List<GramPanchayatDto>>>
{
    public async ValueTask<Result<List<GramPanchayatDto>>> Handle(
        ListGramPanchayatsByBlockQuery request,
        CancellationToken cancellationToken)
    {
        List<GramPanchayatDto> gramPanchayats = await dbContext.GramPanchayats
            .Where(g => g.BlockId == request.BlockId)
            .OrderBy(g => g.GramPanchayatName)
            .Select(g => new GramPanchayatDto(
                g.GramPanchayatId,
                g.BlockId,
                g.GramPanchayatName,
                g.GPCode,
                g.IsActive))
            .ToListAsync(cancellationToken);

        return Result<List<GramPanchayatDto>>.Success(gramPanchayats);
    }
}
