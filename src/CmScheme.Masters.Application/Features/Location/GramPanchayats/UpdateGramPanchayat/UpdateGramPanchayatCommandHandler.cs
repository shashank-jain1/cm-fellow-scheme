using Ardalis.Result;
using Mediator;
using CmScheme.Masters.Core.Data;
using CmScheme.Masters.Core.Entities;

namespace CmScheme.Masters.Application.Features.Location.GramPanchayats.UpdateGramPanchayat;

public sealed class UpdateGramPanchayatCommandHandler(IMastersCommandDbContext dbContext)
    : ICommandHandler<UpdateGramPanchayatCommand, Result>
{
    public async ValueTask<Result> Handle(
        UpdateGramPanchayatCommand request,
        CancellationToken cancellationToken)
    {
        GramPanchayat? gramPanchayat = await dbContext.GramPanchayats
            .FindAsync([request.GramPanchayatId], cancellationToken);

        if (gramPanchayat is null)
        {
            return Result.NotFound("Gram Panchayat not found.");
        }

        gramPanchayat.BlockId = request.BlockId;
        gramPanchayat.GramPanchayatName = request.GramPanchayatName;
        gramPanchayat.GPCode = request.GPCode;
        gramPanchayat.ModifiedOn = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.NoContent();
    }
}
