using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Registration.Application.Features.UserAccount.ListUserAccounts;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Registration.Endpoints.UserAccounts;

public sealed class List
{
    public static async Task<IResult> Handle(
        string? role,
        bool? isActive,
        ISender sender,
        CancellationToken cancellationToken)
    {
        ValueTask<Result<IReadOnlyList<UserAccountListItem>>> result = sender.Send(
            new ListUserAccountsQuery { Role = role, IsActive = isActive },
            cancellationToken);

        return await result.ToApiResultAsync();
    }
}
