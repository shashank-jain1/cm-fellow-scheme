using Ardalis.Result;
using CmScheme.Registration.Application.Features.UserAccount.CreateUserAccount;
using CmScheme.Registration.Core.Entities;
using CmScheme.Registration.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Tests.Registration.Handlers;

public class CreateUserAccountCommandHandlerTests
{
    private static CreateUserAccountCommandHandler CreateHandler(RegistrationCommandDbContext ctx) => new(ctx);

    private static Applicant BuildApplicant() => new()
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

    [Fact]
    public async Task Handle_Valid_Command_Creates_Account_With_Hashed_Password()
    {
        var ctx = TestDbContext.CreateRegistration(c =>
        {
            c.Applicants.Add(BuildApplicant());
            c.SaveChanges();
        });
        var handler = CreateHandler(ctx);
        var applicantId = await ctx.Applicants.Select(a => a.ApplicantId).FirstAsync();

        var result = await handler.Handle(new CreateUserAccountCommand
        {
            ApplicantId = applicantId,
            Username = "rohit123",
            Password = "Secure@123",
            Role = "Fellow",
            CreatedBy = 1
        }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.True(result.Value > 0);
        var account = await ctx.UserAccounts.FirstAsync();
        Assert.Equal("rohit123", account.Username);
        Assert.Equal("Fellow", account.Role);
        Assert.True(account.IsActive);
        Assert.False(string.IsNullOrWhiteSpace(account.PasswordHash));
        Assert.DoesNotContain("Secure@123", account.PasswordHash);
        Assert.Equal(applicantId, account.ApplicantId);
    }

    [Fact]
    public async Task Handle_Missing_Applicant_Returns_NotFound()
    {
        var ctx = TestDbContext.CreateRegistration();
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new CreateUserAccountCommand
        {
            ApplicantId = 999,
            Username = "x",
            Password = "P@ss",
            Role = "Fellow",
            CreatedBy = 1
        }, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.NotFound, result.Status);
        Assert.Contains("Applicant not found.", result.Errors);
    }

    [Fact]
    public async Task Handle_Duplicate_Username_Returns_Conflict()
    {
        var ctx = TestDbContext.CreateRegistration(c =>
        {
            var applicant = BuildApplicant();
            c.Applicants.Add(applicant);
            c.SaveChanges();
            c.UserAccounts.Add(new UserAccount
            {
                ApplicantId = applicant.ApplicantId,
                Username = "rohit123",
                PasswordHash = "hash",
                Role = "Fellow",
                IsActive = true
            });
            c.SaveChanges();
        });
        var handler = CreateHandler(ctx);
        var applicantId = await ctx.Applicants.Select(a => a.ApplicantId).FirstAsync();

        var result = await handler.Handle(new CreateUserAccountCommand
        {
            ApplicantId = applicantId,
            Username = "rohit123",
            Password = "Secure@123",
            Role = "Fellow",
            CreatedBy = 1
        }, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.Conflict, result.Status);
        Assert.Contains("Username already exists.", result.Errors);
        Assert.Equal(1, await ctx.UserAccounts.CountAsync());
    }
}