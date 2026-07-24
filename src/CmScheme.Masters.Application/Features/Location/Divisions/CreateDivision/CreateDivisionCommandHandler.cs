using Ardalis.Result;
using Mediator;
using CmScheme.Masters.Core.Data;
using CmScheme.Masters.Core.Entities;

namespace CmScheme.Masters.Application.Features.Location.Divisions.CreateDivision;

public sealed class CreateDivisionCommandHandler(IMastersCommandDbContext dbContext)
    : ICommandHandler<CreateDivisionCommand, Result<int>>
{
    public async ValueTask<Result<int>> Handle(
        CreateDivisionCommand request,
        CancellationToken cancellationToken)
    {
        Division division = new Division
        {
            StateId = request.StateId,
            DivisionName = request.DivisionName,
            DivisionCode = request.DivisionCode,
            IsActive = true
        };

        dbContext.Divisions.Add(division);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(division.DivisionId);
    }
}
