using Ardalis.Result;
using CmScheme.Registration.Application.Features.UserAccount.ResetPassword;
using CmScheme.Registration.Core.Entities;
using CmScheme.Registration.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Tests.Registration.Handlers;

public class ResetPasswordCommandHandlerTests
{
    private const string ValidToken = "valid-reset-token";

    private static ResetPasswordCommandHandler CreateHandler(RegistrationCommandDbContext ctx) => new(ctx);

    private static RegistrationCommandDbContext BuildContext(
        string? resetToken = ValidToken,
        DateTime? tokenExpiry = null)
    {
        return TestDbContext.CreateRegistration(c =>
        {
            var applicant = new Applicant
            {
                FirstName = "Rohit",
                LastName = "Sharma",
                FatherName = "Father",
                MobileNumber = "9876543210",
                EmailId = "rohit@example.com",
                PermanentAddress = "Mumbai",
                PinCode = "400001",
                BoardUniversityName = "MU",
                Status = "Approved"
            };
            c.Applicants.Add(applicant);
            c.SaveChanges();
            c.UserAccounts.Add(new UserAccount
            {
                ApplicantId = applicant.ApplicantId,
                Username = "rohit123",
                PasswordHash = "old-hash",
                Role = "Fellow",
                IsActive = true,
                PasswordResetToken = resetToken,
                PasswordResetTokenExpiry = tokenExpiry ?? DateTime.UtcNow.AddMinutes(30)
            });
            c.SaveChanges();
        });
    }

    [Fact]
    public async Task Handle_Valid_Token_Updates_PasswordHash()
    {
        var ctx = BuildContext();
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(
            new ResetPasswordCommand { Token = ValidToken, NewPassword = "New@1234" },
            CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(ResultStatus.NoContent, result.Status);
        var account = await ctx.UserAccounts.FirstAsync();
        Assert.NotEqual("old-hash", account.PasswordHash);
        Assert.False(string.IsNullOrWhiteSpace(account.PasswordHash));
    }

    [Fact]
    public async Task Handle_Valid_Token_Clears_Token_So_Link_Cannot_Be_Replayed()
    {
        var ctx = BuildContext();
        var handler = CreateHandler(ctx);

        await handler.Handle(
            new ResetPasswordCommand { Token = ValidToken, NewPassword = "New@1234" },
            CancellationToken.None);

        var account = await ctx.UserAccounts.FirstAsync();
        Assert.Null(account.PasswordResetToken);
        Assert.Null(account.PasswordResetTokenExpiry);

        var replay = await handler.Handle(
            new ResetPasswordCommand { Token = ValidToken, NewPassword = "Another@1234" },
            CancellationToken.None);

        Assert.False(replay.IsSuccess);
    }

    [Fact]
    public async Task Handle_Unknown_Token_Is_Rejected_And_Leaves_Password_Untouched()
    {
        var ctx = BuildContext();
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(
            new ResetPasswordCommand { Token = "not-the-token", NewPassword = "New@1234" },
            CancellationToken.None);

        Assert.False(result.IsSuccess);
        var account = await ctx.UserAccounts.FirstAsync();
        Assert.Equal("old-hash", account.PasswordHash);
    }

    [Fact]
    public async Task Handle_Expired_Token_Is_Rejected()
    {
        var ctx = BuildContext(tokenExpiry: DateTime.UtcNow.AddMinutes(-1));
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(
            new ResetPasswordCommand { Token = ValidToken, NewPassword = "New@1234" },
            CancellationToken.None);

        Assert.False(result.IsSuccess);
        var account = await ctx.UserAccounts.FirstAsync();
        Assert.Equal("old-hash", account.PasswordHash);
    }
}
