using Ardalis.Result;
using Mediator;
using CmScheme.Masters.Core.Data;
using CmScheme.Masters.Core.Entities;

namespace CmScheme.Masters.Application.Features.Location.Districts.UpdateDistrict;

public sealed class UpdateDistrictCommandHandler(IMastersCommandDbContext dbContext)
    : ICommandHandler<UpdateDistrictCommand, Result>
{
    public async ValueTask<Result> Handle(
        UpdateDistrictCommand request,
        CancellationToken cancellationToken)
    {
        District? district = await dbContext.Districts
            .FindAsync([request.DistrictId], cancellationToken);

        if (district is null)
        {
            return Result.NotFound("District not found.");
        }

        district.DivisionId = request.DivisionId;
        district.DistrictName = request.DistrictName;
        district.DistrictCode = request.DistrictCode;
        district.ModifiedOn = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.NoContent();
    }
}
