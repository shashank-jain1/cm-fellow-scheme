namespace CmScheme.Common.Core.Data;

public interface IUserInfoAccessor
{
    int? UserId { get; }
    string? UserName { get; }
}
