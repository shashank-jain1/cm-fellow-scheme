using System.Security.Cryptography;
using Ardalis.Result;
using CmScheme.Registration.Application.Features.UserAccount.Login;
using CmScheme.Registration.Core.Entities;
using CmScheme.Registration.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Tests.Registration.Handlers;

public class LoginCommandHandlerTests
{
    private static LoginCommandHandler CreateHandler(RegistrationCommandDbContext ctx) => new(ctx);

    private static string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(16);
        using Rfc2898DeriveBytes pbkdf2 = new(password, salt, 100000, HashAlgorithmName.SHA256);
        byte[] hash = pbkdf2.GetBytes(32);
        byte[] hashBytes = new byte[48];
        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 32);
        return Convert.ToBase64String(hashBytes);
    }

    private static RegistrationCommandDbContext BuildContext(string password, bool isActive = true)
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
                PasswordHash = HashPassword(password),
                Role = "Fellow",
                IsActive = isActive
            });
            c.SaveChanges();
        });
    }

    [Fact]
    public async Task Handle_Valid_Credentials_Returns_LoginResult()
    {
        var ctx = BuildContext("Fellow@123");
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new LoginCommand { Username = "rohit123", Password = "Fellow@123" }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal("rohit123", result.Value.Username);
        Assert.Equal("Fellow", result.Value.Role);
        Assert.True(result.Value.UserAccountId > 0);
    }

    [Fact]
    public async Task Handle_Wrong_Password_Returns_NotFound()
    {
        var ctx = BuildContext("Fellow@123");
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new LoginCommand { Username = "rohit123", Password = "Wrong@123" }, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.NotFound, result.Status);
        Assert.Contains("Invalid username or password.", result.Errors);
    }

    [Fact]
    public async Task Handle_Unknown_Username_Returns_NotFound()
    {
        var ctx = BuildContext("Fellow@123");
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new LoginCommand { Username = "nobody", Password = "Fellow@123" }, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.NotFound, result.Status);
    }

    [Fact]
    public async Task Handle_Inactive_Account_Returns_NotFound()
    {
        var ctx = BuildContext("Fellow@123", isActive: false);
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new LoginCommand { Username = "rohit123", Password = "Fellow@123" }, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.NotFound, result.Status);
    }
}