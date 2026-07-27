using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Masters.Core.Data;
using CmScheme.Masters.Core.Dtos;

namespace CmScheme.Masters.Application.Features.Location.Districts.ListDistrictsByDivision;

public sealed class ListDistrictsByDivisionQueryHandler(IMastersQueryDbContext dbContext)
    : IQueryHandler<ListDistrictsByDivisionQuery, Result<List<DistrictDto>>>
{
    public async ValueTask<Result<List<DistrictDto>>> Handle(
        ListDistrictsByDivisionQuery request,
        CancellationToken cancellationToken)
    {
        IQueryable<Core.Entities.District> query = dbContext.Districts
            .AsNoTracking();

        if (request.DivisionId.HasValue)
        {
            query = query.Where(d => d.DivisionId == request.DivisionId.Value);
        }

        List<DistrictDto> districts = await query
            .OrderBy(d => d.DistrictName)
            .Select(d => new DistrictDto(
                d.DistrictId,
                d.DivisionId,
                d.DistrictName,
                d.DistrictCode,
                d.IsActive))
            .ToListAsync(cancellationToken);

        return Result<List<DistrictDto>>.Success(districts);
    }
}
