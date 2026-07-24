using Ardalis.Result;
using Mediator;
using CmScheme.Masters.Core.Data;
using CmScheme.Masters.Core.Entities;

namespace CmScheme.Masters.Application.Features.Location.GramPanchayats.CreateGramPanchayat;

public sealed class CreateGramPanchayatCommandHandler(IMastersCommandDbContext dbContext)
    : ICommandHandler<CreateGramPanchayatCommand, Result<int>>
{
    public async ValueTask<Result<int>> Handle(
        CreateGramPanchayatCommand request,
        CancellationToken cancellationToken)
    {
        GramPanchayat gramPanchayat = new GramPanchayat
        {
            BlockId = request.BlockId,
            GramPanchayatName = request.GramPanchayatName,
            GPCode = request.GPCode,
            IsActive = true
        };

        dbContext.GramPanchayats.Add(gramPanchayat);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(gramPanchayat.GramPanchayatId);
    }
}
