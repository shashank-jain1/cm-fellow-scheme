using System.Security.Cryptography;
using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Registration.Core.Data;

namespace CmScheme.Registration.Application.Features.UserAccount.Login;

public sealed record LoginCommand : ICommand<Result<LoginResult>>
{
    public string Username { get; init; } = null!;
    public string Password { get; init; } = null!;
}

public sealed record LoginResult
{
    public int UserAccountId { get; init; }
    public string Username { get; init; } = null!;
    public string Role { get; init; } = null!;
}
