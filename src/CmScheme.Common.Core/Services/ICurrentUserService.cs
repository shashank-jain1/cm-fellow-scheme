namespace CmScheme.Common.Core.Services;

public interface ICurrentUserService
{
    int? UserAccountId { get; }

    string? Username { get; }

    string? Role { get; }

    bool IsAdmin { get; }
}
