using Ardalis.Result;
using Mediator;

namespace CmScheme.Registration.Application.Features.UserAccount.ListUserAccounts;

public sealed record ListUserAccountsQuery : IQuery<Result<IReadOnlyList<UserAccountListItem>>>
{
    public string? Role { get; init; }
    public bool? IsActive { get; init; }
}

public sealed record UserAccountListItem
{
    public int UserAccountId { get; init; }
    public int ApplicantId { get; init; }
    public string Username { get; init; } = null!;
    public string Role { get; init; } = null!;
    public bool IsActive { get; init; }
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string EmailId { get; init; } = null!;
    public string MobileNumber { get; init; } = null!;
    public DateTime CreatedOn { get; init; }
}
