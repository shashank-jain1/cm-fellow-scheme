using Ardalis.Result;
using Mediator;
using CmScheme.Masters.Core.Data;
using CmScheme.Masters.Core.Entities;

namespace CmScheme.Masters.Application.Features.Location.Divisions.DeleteDivision;

public sealed class DeleteDivisionCommandHandler(IMastersCommandDbContext dbContext)
    : ICommandHandler<DeleteDivisionCommand, Result>
{
    public async ValueTask<Result> Handle(
        DeleteDivisionCommand request,
        CancellationToken cancellationToken)
    {
        Division? division = await dbContext.Divisions.FindAsync(
            new object[] { request.DivisionId },
            cancellationToken);

        if (division is null)
            return Result.NotFound("Division not found.");

        division.IsActive = false;
        division.ModifiedOn = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.NoContent();
    }
}
