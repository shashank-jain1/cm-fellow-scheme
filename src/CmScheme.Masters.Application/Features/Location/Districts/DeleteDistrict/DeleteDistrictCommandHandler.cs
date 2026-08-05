using Ardalis.Result;
using Mediator;
using CmScheme.Masters.Core.Data;
using CmScheme.Masters.Core.Entities;

namespace CmScheme.Masters.Application.Features.Location.Districts.DeleteDistrict;

public sealed class DeleteDistrictCommandHandler(IMastersCommandDbContext dbContext)
    : ICommandHandler<DeleteDistrictCommand, Result>
{
    public async ValueTask<Result> Handle(
        DeleteDistrictCommand request,
        CancellationToken cancellationToken)
    {
        District? district = await dbContext.Districts
            .FindAsync([request.DistrictId], cancellationToken);

        if (district is null)
        {
            return Result.NotFound("District not found.");
        }

        district.IsActive = false;
        district.ModifiedOn = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.NoContent();
    }
}
