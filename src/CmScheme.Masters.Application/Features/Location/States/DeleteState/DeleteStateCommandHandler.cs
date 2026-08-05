using Ardalis.Result;
using CmScheme.Masters.Core.Data;
using CmScheme.Masters.Core.Entities;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Masters.Application.Features.Location.States.DeleteState;

public sealed class DeleteStateCommandHandler(IMastersCommandDbContext dbContext)
    : ICommandHandler<DeleteStateCommand, Result>
{
    public async ValueTask<Result> Handle(DeleteStateCommand request, CancellationToken cancellationToken)
    {
        State? state = await dbContext.States
            .FirstOrDefaultAsync(s => s.StateId == request.StateId, cancellationToken);

        if (state is null)
        {
            return Result.NotFound("State not found.");
        }

        state.IsActive = false;
        state.ModifiedOn = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.NoContent();
    }
}
