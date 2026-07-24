using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Registration.Core.Data;
using CmScheme.Registration.Core.Entities;

namespace CmScheme.Registration.Application.Features.UserAccount.DeactivateUserAccount;

public sealed class DeactivateUserAccountCommandHandler(IRegistrationCommandDbContext dbContext)
    : ICommandHandler<DeactivateUserAccountCommand, Result>
{
    public async ValueTask<Result> Handle(
        DeactivateUserAccountCommand request,
        CancellationToken cancellationToken)
    {
        Core.Entities.UserAccount? userAccount = await dbContext.UserAccounts
            .FirstOrDefaultAsync(ua => ua.UserAccountId == request.UserAccountId, cancellationToken);

        if (userAccount is null)
        {
            return Result.NotFound("User account not found.");
        }

        userAccount.IsActive = false;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.NoContent();
    }
}
