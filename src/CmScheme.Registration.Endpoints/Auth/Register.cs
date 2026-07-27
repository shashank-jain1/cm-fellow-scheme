using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Registration.Application.Features.UserAccount.CreateUserAccount;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Registration.Endpoints.Auth;

public sealed class RegisterRequest
{
    public int ApplicantId { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Role { get; set; } = "Fellow";
}

public sealed class Register
{
    public static async Task<IResult> Handle(
        RegisterRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        ValueTask<Result<int>> result = sender.Send(
            new CreateUserAccountCommand
            {
                ApplicantId = request.ApplicantId,
                Username = request.Username,
                Password = request.Password,
                Role = request.Role,
                CreatedBy = 0,
            },
            cancellationToken);

        return await result.ToApiResultAsync();
    }
}
