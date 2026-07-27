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
        IQueryable<Core.Entities.GramPanchayat> query = dbContext.GramPanchayats
            .AsNoTracking();

        if (request.BlockId.HasValue)
        {
            query = query.Where(g => g.BlockId == request.BlockId.Value);
        }

        List<GramPanchayatDto> gramPanchayats = await query
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
