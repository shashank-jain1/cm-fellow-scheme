using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Registration.Core.Data;
using CmScheme.Registration.Core.Entities;

namespace CmScheme.Registration.Application.Features.UserAccount.AssignRole;

public sealed class AssignRoleCommandHandler(IRegistrationCommandDbContext dbContext)
    : ICommandHandler<AssignRoleCommand, Result>
{
    public async ValueTask<Result> Handle(
        AssignRoleCommand request,
        CancellationToken cancellationToken)
    {
        Core.Entities.UserAccount? userAccount = await dbContext.UserAccounts
            .FirstOrDefaultAsync(ua => ua.UserAccountId == request.UserAccountId, cancellationToken);

        if (userAccount is null)
        {
            return Result.NotFound("User account not found.");
        }

        userAccount.Role = request.Role;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.NoContent();
    }
}
