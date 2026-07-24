using Ardalis.Result;
using Mediator;
using CmScheme.Masters.Core.Data;
using CmScheme.Masters.Core.Entities;

namespace CmScheme.Masters.Application.Features.Location.States.CreateState;

public sealed class CreateStateCommandHandler(IMastersCommandDbContext dbContext)
    : ICommandHandler<CreateStateCommand, Result<int>>
{
    public async ValueTask<Result<int>> Handle(
        CreateStateCommand request,
        CancellationToken cancellationToken)
    {
        State state = new State
        {
            StateName = request.StateName,
            StateCode = request.StateCode,
            StateShortName = request.StateShortName,
            DisplayOrder = request.DisplayOrder,
            IsActive = true
        };

        dbContext.States.Add(state);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(state.StateId);
    }
}
