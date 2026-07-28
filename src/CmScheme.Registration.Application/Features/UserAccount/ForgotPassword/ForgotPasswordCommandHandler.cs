using System.Security.Cryptography;
using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Registration.Core.Data;
using UserAccountEntity = CmScheme.Registration.Core.Entities.UserAccount;

namespace CmScheme.Registration.Application.Features.UserAccount.ForgotPassword;

public sealed class ForgotPasswordCommandHandler(IRegistrationCommandDbContext dbContext)
    : ICommandHandler<ForgotPasswordCommand, Result<ForgotPasswordResponse>>
{
    public async ValueTask<Result<ForgotPasswordResponse>> Handle(
        ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        UserAccountEntity? userAccount = await dbContext.UserAccounts
            .FirstOrDefaultAsync(ua => ua.Username == request.Email || ua.Applicant!.EmailId == request.Email, cancellationToken);

        if (userAccount is null)
        {
            return Result.NotFound("No account found with this email.");
        }

        string resetToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));

        return Result.Success(new ForgotPasswordResponse
        {
            Message = "If an account exists with this email, a reset link has been sent.",
            ResetToken = resetToken
        });
    }
}
