using Ardalis.Result;
using CmScheme.Common.Core;
using CmScheme.Common.Core.Services;
using CmScheme.Registration.Application.Features.Registration.BulkApprove;
using CmScheme.Registration.Core.Entities;
using CmScheme.Registration.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CmScheme.Tests.Registration.Handlers;

public class BulkApproveRegistrationCommandHandlerTests
{
    private static BulkApproveRegistrationCommandHandler CreateHandler(
        RegistrationCommandDbContext ctx,
        INotificationService notifications) => new(ctx, notifications);

    private static Applicant BuildApplicant(string mobile, string status = Statuses.Registration.Pending) => new()
    {
        FirstName = "Rohit",
        LastName = "Sharma",
        FatherName = "Kishan",
        MobileNumber = mobile,
        EmailId = $"{mobile}@example.com",
        PermanentAddress = "Mumbai",
        PinCode = "400001",
        BoardUniversityName = "MU",
        Status = status,
        AppliedForTraining = 1
    };

    [Fact]
    public async Task Handle_Approve_Action_Approves_Pending_Applicants_And_Creates_Accounts()
    {
        var ctx = TestDbContext.CreateRegistration(c =>
        {
            c.Applicants.AddRange(BuildApplicant("1111111111"), BuildApplicant("2222222222"), BuildApplicant("3333333333", Statuses.Registration.Approved));
            c.SaveChanges();
        });
        var notifications = new Mock<INotificationService>();
        var handler = CreateHandler(ctx, notifications.Object);
        var ids = await ctx.Applicants.Select(a => a.ApplicantId).ToListAsync();

        var result = await handler.Handle(new BulkApproveRegistrationCommand
        {
            ApplicantIds = ids,
            Action = "Approve",
            PerformedBy = 1
        }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Value);
        Assert.Equal(2, await ctx.UserAccounts.CountAsync());
        foreach (var applicant in ctx.Applicants)
        {
            Assert.Equal(Statuses.Registration.Approved, applicant.Status);
        }
        notifications.Verify(n => n.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
    }

    [Fact]
    public async Task Handle_Reject_Action_Rejects_Pending_Applicants()
    {
        var ctx = TestDbContext.CreateRegistration(c =>
        {
            c.Applicants.AddRange(BuildApplicant("1111111111"), BuildApplicant("2222222222", Statuses.Registration.Rejected));
            c.SaveChanges();
        });
        var notifications = new Mock<INotificationService>();
        var handler = CreateHandler(ctx, notifications.Object);
        var ids = await ctx.Applicants.Select(a => a.ApplicantId).ToListAsync();

        var result = await handler.Handle(new BulkApproveRegistrationCommand
        {
            ApplicantIds = ids,
            Action = "Reject",
            PerformedBy = 1
        }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(1, result.Value);
        Assert.Empty(ctx.UserAccounts);
        Assert.All(ctx.Applicants, a => Assert.Equal(Statuses.Registration.Rejected, a.Status));
        notifications.Verify(n => n.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_Empty_ApplicantIds_Returns_Invalid()
    {
        var ctx = TestDbContext.CreateRegistration();
        var handler = CreateHandler(ctx, Mock.Of<INotificationService>());

        var result = await handler.Handle(new BulkApproveRegistrationCommand
        {
            ApplicantIds = new List<int>(),
            Action = "Approve",
            PerformedBy = 1
        }, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.Invalid, result.Status);
        Assert.Contains("At least one Applicant ID is required.", result.ValidationErrors.Select(e => e.ErrorMessage));
    }

    [Fact]
    public async Task Handle_Invalid_Action_Returns_Invalid()
    {
        var ctx = TestDbContext.CreateRegistration();
        var handler = CreateHandler(ctx, Mock.Of<INotificationService>());

        var result = await handler.Handle(new BulkApproveRegistrationCommand
        {
            ApplicantIds = new[] { 1 },
            Action = "Delete",
            PerformedBy = 1
        }, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.Invalid, result.Status);
        Assert.Contains("Action must be 'Approve' or 'Reject'.", result.ValidationErrors.Select(e => e.ErrorMessage));
    }
}