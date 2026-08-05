using Ardalis.Result;
using CmScheme.Masters.Core.Data;
using CmScheme.Masters.Core.Entities;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Masters.Application.Features.Location.States.UpdateState;

public sealed class UpdateStateCommandHandler(IMastersCommandDbContext dbContext)
    : ICommandHandler<UpdateStateCommand, Result>
{
    public async ValueTask<Result> Handle(UpdateStateCommand request, CancellationToken cancellationToken)
    {
        State? state = await dbContext.States
            .FirstOrDefaultAsync(s => s.StateId == request.StateId, cancellationToken);

        if (state is null)
        {
            return Result.NotFound("State not found.");
        }

        state.StateName = request.StateName;
        state.StateCode = request.StateCode;
        state.StateShortName = request.StateShortName;
        state.DisplayOrder = request.DisplayOrder;
        state.IsActive = request.IsActive;
        state.ModifiedOn = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.NoContent();
    }
}
