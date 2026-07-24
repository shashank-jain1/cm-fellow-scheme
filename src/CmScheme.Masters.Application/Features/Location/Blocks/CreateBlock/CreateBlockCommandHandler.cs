using Ardalis.Result;
using Mediator;
using CmScheme.Masters.Core.Data;
using CmScheme.Masters.Core.Entities;

namespace CmScheme.Masters.Application.Features.Location.Blocks.CreateBlock;

public sealed class CreateBlockCommandHandler(IMastersCommandDbContext dbContext)
    : ICommandHandler<CreateBlockCommand, Result<int>>
{
    public async ValueTask<Result<int>> Handle(
        CreateBlockCommand request,
        CancellationToken cancellationToken)
    {
        Block block = new Block
        {
            DistrictId = request.DistrictId,
            BlockName = request.BlockName,
            BlockCode = request.BlockCode,
            IsActive = true
        };

        dbContext.Blocks.Add(block);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(block.BlockId);
    }
}
