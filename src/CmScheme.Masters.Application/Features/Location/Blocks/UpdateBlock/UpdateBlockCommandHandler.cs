using Ardalis.Result;
using Mediator;
using CmScheme.Masters.Core.Data;
using CmScheme.Masters.Core.Entities;

namespace CmScheme.Masters.Application.Features.Location.Blocks.UpdateBlock;

public sealed class UpdateBlockCommandHandler(IMastersCommandDbContext dbContext)
    : ICommandHandler<UpdateBlockCommand, Result>
{
    public async ValueTask<Result> Handle(
        UpdateBlockCommand request,
        CancellationToken cancellationToken)
    {
        Block? block = await dbContext.Blocks
            .FindAsync([request.BlockId], cancellationToken);

        if (block is null)
        {
            return Result.NotFound("Block not found.");
        }

        block.DistrictId = request.DistrictId;
        block.BlockName = request.BlockName;
        block.BlockCode = request.BlockCode;
        block.ModifiedOn = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.NoContent();
    }
}
