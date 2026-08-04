using Ardalis.Result;
using CmScheme.Masters.Core.Data;
using CmScheme.Masters.Core.Entities;
using Mediator;

namespace CmScheme.Masters.Application.Features.LookupMaster.CreateLookupMaster;

public sealed class CreateLookupMasterCommandHandler(IMastersCommandDbContext dbContext)
    : ICommandHandler<CreateLookupMasterCommand, Result<int>>
{
    public async ValueTask<Result<int>> Handle(CreateLookupMasterCommand request, CancellationToken cancellationToken)
    {
        Core.Entities.LookupMaster entity = new Core.Entities.LookupMaster
        {
            MasterType = request.MasterType,
            Label = request.Label,
            Value = request.Value,
            SortOrder = request.SortOrder,
            IsActive = true,
            CreatedOn = DateTime.UtcNow
        };

        dbContext.LookupMasters.Add(entity);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(entity.LookupMasterId);
    }
}
