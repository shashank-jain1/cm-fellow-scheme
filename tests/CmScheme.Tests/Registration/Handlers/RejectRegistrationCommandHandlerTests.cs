using Ardalis.Result;
using CmScheme.Common.Core;
using CmScheme.Common.Core.Services;
using CmScheme.Registration.Application.Features.Registration.RejectRegistration;
using CmScheme.Registration.Core.Entities;
using CmScheme.Registration.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CmScheme.Tests.Registration.Handlers;

public class RejectRegistrationCommandHandlerTests
{
    private static RejectRegistrationCommandHandler CreateHandler(
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
    public async Task Handle_Pending_Applicant_Rejects_And_Updates_Status()
    {
        var ctx = TestDbContext.CreateRegistration(c =>
        {
            c.Applicants.Add(BuildApplicant());
            c.SaveChanges();
        });
        var notifications = new Mock<INotificationService>();
        var handler = CreateHandler(ctx, notifications.Object);
        var applicantId = await ctx.Applicants.Select(a => a.ApplicantId).FirstAsync();

        var result = await handler.Handle(new RejectRegistrationCommand { ApplicantId = applicantId, Reason = "criteria", RejectedBy = 2 }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(ResultStatus.NoContent, result.Status);
        var applicant = await ctx.Applicants.FirstAsync();
        Assert.Equal(Statuses.Registration.Rejected, applicant.Status);
        Assert.Equal(2, applicant.ModifiedBy);
        notifications.Verify(n => n.SendEmailAsync("rohit@example.com", It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        notifications.Verify(n => n.SendSmsAsync("9876543210", It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Missing_Applicant_Returns_NotFound()
    {
        var ctx = TestDbContext.CreateRegistration();
        var handler = CreateHandler(ctx, Mock.Of<INotificationService>());

        var result = await handler.Handle(new RejectRegistrationCommand { ApplicantId = 999, Reason = "x", RejectedBy = 1 }, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.NotFound, result.Status);
        Assert.Contains("Applicant not found.", result.Errors);
    }

    [Fact]
    public async Task Handle_Already_Rejected_Returns_Conflict()
    {
        var ctx = TestDbContext.CreateRegistration(c =>
        {
            c.Applicants.Add(BuildApplicant(Statuses.Registration.Rejected));
            c.SaveChanges();
        });
        var notifications = new Mock<INotificationService>();
        var handler = CreateHandler(ctx, notifications.Object);
        var applicantId = await ctx.Applicants.Select(a => a.ApplicantId).FirstAsync();

        var result = await handler.Handle(new RejectRegistrationCommand { ApplicantId = applicantId, Reason = "x", RejectedBy = 1 }, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.Conflict, result.Status);
        Assert.Contains("Registration is already rejected.", result.Errors);
        notifications.Verify(n => n.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}