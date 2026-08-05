using Ardalis.Result;
using Mediator;
using CmScheme.Masters.Core.Data;
using CmScheme.Masters.Core.Entities;

namespace CmScheme.Masters.Application.Features.Location.Blocks.DeleteBlock;

public sealed class DeleteBlockCommandHandler(IMastersCommandDbContext dbContext)
    : ICommandHandler<DeleteBlockCommand, Result>
{
    public async ValueTask<Result> Handle(
        DeleteBlockCommand request,
        CancellationToken cancellationToken)
    {
        Block? block = await dbContext.Blocks
            .FindAsync([request.BlockId], cancellationToken);

        if (block is null)
        {
            return Result.NotFound("Block not found.");
        }

        block.IsActive = false;
        block.ModifiedOn = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.NoContent();
    }
}
