using Ardalis.Result;
using CmScheme.Common.Core;
using CmScheme.Common.Core.Services;
using CmScheme.Registration.Application.Features.Registration.ApproveRegistration;
using CmScheme.Registration.Core.Entities;
using CmScheme.Registration.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CmScheme.Tests.Registration.Handlers;

public class ApproveRegistrationCommandHandlerTests
{
    private static ApproveRegistrationCommandHandler CreateHandler(
        RegistrationCommandDbContext ctx,
        INotificationService notifications) => new(ctx, notifications);

    private static Applicant BuildApplicant(string status = Statuses.Registration.Pending) => new()
    {
        FirstName = "Rohit",
        LastName = "Sharma",
        FatherName = "Kishan",
        MobileNumber = "9876543210",
        EmailId = "rohit@example.com",
        PermanentAddress = "Mumbai",
        PinCode = "400001",
        BoardUniversityName = "MU",
        Status = status
    };

    [Fact]
    public async Task Handle_Pending_Applicant_Approves_And_Creates_UserAccount()
    {
        var ctx = TestDbContext.CreateRegistration(c =>
        {
            c.Applicants.Add(BuildApplicant());
            c.SaveChanges();
        });
        var notifications = new Mock<INotificationService>();
        var handler = CreateHandler(ctx, notifications.Object);
        var applicantId = await ctx.Applicants.Select(a => a.ApplicantId).FirstAsync();

        var result = await handler.Handle(new ApproveRegistrationCommand { ApplicantId = applicantId, ApprovedBy = 1 }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(ResultStatus.NoContent, result.Status);
        var applicant = await ctx.Applicants.FirstAsync();
        Assert.Equal(Statuses.Registration.Approved, applicant.Status);
        Assert.Equal(1, applicant.ModifiedBy);
        var account = await ctx.UserAccounts.FirstAsync();
        Assert.Equal(applicantId, account.ApplicantId);
        Assert.Equal("9876543210", account.Username);
        Assert.Equal("Fellow", account.Role);
        Assert.True(account.IsActive);
        Assert.False(string.IsNullOrWhiteSpace(account.PasswordHash));
        Assert.DoesNotContain("Fellow@123", account.PasswordHash);
        notifications.Verify(n => n.SendEmailAsync("rohit@example.com", It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        notifications.Verify(n => n.SendSmsAsync("9876543210", It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Supervisor_Training_Role_Is_Supervisor()
    {
        var ctx = TestDbContext.CreateRegistration(c =>
        {
            c.Applicants.Add(new Applicant { FirstName = "Rohit", LastName = "Sharma", FatherName = "Kishan", MobileNumber = "9876543210", EmailId = "rohit@example.com", PermanentAddress = "Mumbai", PinCode = "400001", BoardUniversityName = "MU", Status = Statuses.Registration.Pending, AppliedForTraining = 2 });
            c.SaveChanges();
        });
        var notifications = new Mock<INotificationService>();
        var handler = CreateHandler(ctx, notifications.Object);
        var applicantId = await ctx.Applicants.Select(a => a.ApplicantId).FirstAsync();

        var result = await handler.Handle(new ApproveRegistrationCommand { ApplicantId = applicantId, ApprovedBy = 1 }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        var account = await ctx.UserAccounts.FirstAsync();
        Assert.Equal("Supervisor", account.Role);
    }

    [Fact]
    public async Task Handle_Missing_Applicant_Returns_NotFound()
    {
        var ctx = TestDbContext.CreateRegistration();
        var handler = CreateHandler(ctx, Mock.Of<INotificationService>());

        var result = await handler.Handle(new ApproveRegistrationCommand { ApplicantId = 999, ApprovedBy = 1 }, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.NotFound, result.Status);
        Assert.Contains("Applicant not found.", result.Errors);
        Assert.Empty(ctx.UserAccounts);
    }

    [Fact]
    public async Task Handle_Already_Approved_Returns_Conflict()
    {
        var ctx = TestDbContext.CreateRegistration(c =>
        {
            c.Applicants.Add(BuildApplicant(Statuses.Registration.Approved));
            c.SaveChanges();
        });
        var notifications = new Mock<INotificationService>();
        var handler = CreateHandler(ctx, notifications.Object);
        var applicantId = await ctx.Applicants.Select(a => a.ApplicantId).FirstAsync();

        var result = await handler.Handle(new ApproveRegistrationCommand { ApplicantId = applicantId, ApprovedBy = 1 }, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.Conflict, result.Status);
        Assert.Contains("Registration is already approved.", result.Errors);
        notifications.Verify(n => n.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}