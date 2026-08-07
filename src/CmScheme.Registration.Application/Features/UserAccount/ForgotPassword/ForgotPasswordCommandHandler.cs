using System.Security.Cryptography;
using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Common.Core.Services;
using CmScheme.Registration.Core.Data;
using UserAccountEntity = CmScheme.Registration.Core.Entities.UserAccount;

namespace CmScheme.Registration.Application.Features.UserAccount.ForgotPassword;

public sealed class ForgotPasswordCommandHandler(
    IRegistrationCommandDbContext dbContext,
    INotificationService notificationService)
    : ICommandHandler<ForgotPasswordCommand, Result<ForgotPasswordResponse>>
{
    public async ValueTask<Result<ForgotPasswordResponse>> Handle(
        ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        UserAccountEntity? userAccount = await dbContext.UserAccounts
            .Include(ua => ua.Applicant)
            .FirstOrDefaultAsync(ua => ua.Username == request.Email || ua.Applicant!.EmailId == request.Email, cancellationToken);

        if (userAccount is null)
        {
            return Result.NotFound("No account found with this email.");
        }

        string resetToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        DateTime expiry = DateTime.UtcNow.AddHours(1);

        userAccount.PasswordResetToken = resetToken;
        userAccount.PasswordResetTokenExpiry = expiry;
        await dbContext.SaveChangesAsync(cancellationToken);

        string? recipientEmail = userAccount.Applicant?.EmailId ?? userAccount.Username;
        string resetLink = $"https://cmportal.gov.in/reset-password?token={Uri.EscapeDataString(resetToken)}";
        string emailBody = $"""
            <h2>Password Reset Request</h2>
            <p>You requested a password reset for your CM Fellowship account.</p>
            <p>Click the link below to reset your password. This link expires in 1 hour.</p>
            <p><a href="{resetLink}" style="background:#1a73e8;color:white;padding:10px 20px;text-decoration:none;border-radius:4px;">Reset Password</a></p>
            <p>If you did not request this, please ignore this email.</p>
            """;

        await notificationService.SendEmailAsync(recipientEmail!, "CM Portal - Password Reset", emailBody, cancellationToken);

        return Result.Success(new ForgotPasswordResponse
        {
            Message = "If an account exists with this email, a reset link has been sent.",
            ResetToken = null
        });
    }
}
