using Ardalis.Result;
using Mediator;
using CmScheme.Masters.Core.Data;
using CmScheme.Masters.Core.Entities;

namespace CmScheme.Masters.Application.Features.Location.Districts.CreateDistrict;

public sealed class CreateDistrictCommandHandler(IMastersCommandDbContext dbContext)
    : ICommandHandler<CreateDistrictCommand, Result<int>>
{
    public async ValueTask<Result<int>> Handle(
        CreateDistrictCommand request,
        CancellationToken cancellationToken)
    {
        District district = new District
        {
            DivisionId = request.DivisionId,
            DistrictName = request.DistrictName,
            DistrictCode = request.DistrictCode,
            IsActive = true
        };

        dbContext.Districts.Add(district);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(district.DistrictId);
    }
}
