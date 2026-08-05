using Ardalis.Result;
using Mediator;
using CmScheme.Masters.Core.Data;
using CmScheme.Masters.Core.Entities;

namespace CmScheme.Masters.Application.Features.Location.Divisions.UpdateDivision;

public sealed class UpdateDivisionCommandHandler(IMastersCommandDbContext dbContext)
    : ICommandHandler<UpdateDivisionCommand, Result>
{
    public async ValueTask<Result> Handle(
        UpdateDivisionCommand request,
        CancellationToken cancellationToken)
    {
        Division? division = await dbContext.Divisions.FindAsync(
            new object[] { request.DivisionId },
            cancellationToken);

        if (division is null)
            return Result.NotFound("Division not found.");

        division.StateId = request.StateId;
        division.DivisionName = request.DivisionName;
        division.DivisionCode = request.DivisionCode;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.NoContent();
    }
}
