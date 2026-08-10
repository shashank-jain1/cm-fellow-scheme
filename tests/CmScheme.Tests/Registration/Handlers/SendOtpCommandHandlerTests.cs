using Ardalis.Result;
using CmScheme.Common.Core.Services;
using CmScheme.Registration.Application.Features.Registration.SendOtp;
using CmScheme.Registration.Core.Entities;
using CmScheme.Registration.Infrastructure;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace CmScheme.Tests.Registration.Handlers;

public class SendOtpCommandHandlerTests
{
    private const string Mobile = "9876543210";

    private static SendOtpCommandHandler CreateHandler(
        RegistrationCommandDbContext ctx,
        IOtpService otpService,
        ISmsService smsService,
        INotificationService notifications) =>
        new(ctx, otpService, smsService, notifications, NullLogger<SendOtpCommandHandler>.Instance);

    private static Applicant BuildApplicant() => new()
    {
        FirstName = "Rohit",
        LastName = "Sharma",
        FatherName = "Father",
        MobileNumber = Mobile,
        EmailId = "rohit@example.com",
        PermanentAddress = "Mumbai",
        PinCode = "400001",
        BoardUniversityName = "MU",
        Status = "Pending"
    };

    [Fact]
    public async Task Handle_Generates_Otp_Keyed_By_Mobile_And_Sends_Sms()
    {
        var ctx = TestDbContext.CreateRegistration();
        var otp = new Mock<IOtpService>();
        otp.Setup(o => o.GenerateOtpAsync(Mobile, It.IsAny<CancellationToken>())).ReturnsAsync("123456");
        var sms = new Mock<ISmsService>();
        var notifications = new Mock<INotificationService>();
        var handler = CreateHandler(ctx, otp.Object, sms.Object, notifications.Object);

        var result = await handler.Handle(new SendOtpCommand { MobileNumber = Mobile }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(ResultStatus.NoContent, result.Status);
        otp.Verify(o => o.GenerateOtpAsync(Mobile, It.IsAny<CancellationToken>()), Times.Once);
        sms.Verify(s => s.SendSmsAsync(Mobile, It.Is<string>(m => m.Contains("123456")), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Succeeds_When_No_Applicant_Exists_Yet()
    {
        // OTP verification happens during the wizard, before the applicant row is created.
        var ctx = TestDbContext.CreateRegistration();
        var otp = new Mock<IOtpService>();
        otp.Setup(o => o.GenerateOtpAsync(Mobile, It.IsAny<CancellationToken>())).ReturnsAsync("123456");
        var notifications = new Mock<INotificationService>();
        var handler = CreateHandler(ctx, otp.Object, Mock.Of<ISmsService>(), notifications.Object);

        var result = await handler.Handle(new SendOtpCommand { MobileNumber = Mobile }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        notifications.Verify(
            n => n.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task Handle_Also_Emails_The_Otp_When_The_Mobile_Belongs_To_An_Applicant()
    {
        var ctx = TestDbContext.CreateRegistration(c =>
        {
            c.Applicants.Add(BuildApplicant());
            c.SaveChanges();
        });
        var otp = new Mock<IOtpService>();
        otp.Setup(o => o.GenerateOtpAsync(Mobile, It.IsAny<CancellationToken>())).ReturnsAsync("123456");
        var notifications = new Mock<INotificationService>();
        var handler = CreateHandler(ctx, otp.Object, Mock.Of<ISmsService>(), notifications.Object);

        var result = await handler.Handle(new SendOtpCommand { MobileNumber = Mobile }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        notifications.Verify(
            n => n.SendEmailAsync("rohit@example.com", It.IsAny<string>(), It.Is<string>(b => b.Contains("123456")), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_Still_Succeeds_When_Email_Delivery_Fails()
    {
        var ctx = TestDbContext.CreateRegistration(c =>
        {
            c.Applicants.Add(BuildApplicant());
            c.SaveChanges();
        });
        var otp = new Mock<IOtpService>();
        otp.Setup(o => o.GenerateOtpAsync(Mobile, It.IsAny<CancellationToken>())).ReturnsAsync("123456");
        var notifications = new Mock<INotificationService>();
        notifications
            .Setup(n => n.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("smtp down"));
        var sms = new Mock<ISmsService>();
        var handler = CreateHandler(ctx, otp.Object, sms.Object, notifications.Object);

        var result = await handler.Handle(new SendOtpCommand { MobileNumber = Mobile }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        sms.Verify(s => s.SendSmsAsync(Mobile, It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    /// <summary>
    /// Guards the defect where SendOtp stored the code under the email address while
    /// VerifyMobileOtp looked it up by mobile number, so verification could never succeed.
    /// </summary>
    [Fact]
    public async Task Send_And_Verify_Use_The_Same_Otp_Key()
    {
        var ctx = TestDbContext.CreateRegistration(c =>
        {
            c.Applicants.Add(BuildApplicant());
            c.SaveChanges();
        });
        IOtpService realOtpService = new CmScheme.Common.Infrastructure.Services.InMemoryOtpService(
            NullLogger<CmScheme.Common.Infrastructure.Services.InMemoryOtpService>.Instance);

        string? captured = null;
        var sms = new Mock<ISmsService>();
        sms.Setup(s => s.SendSmsAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
           .Callback<string, string, CancellationToken>((_, message, _) =>
               captured = new string(message.Where(char.IsDigit).ToArray()).Substring(0, 6))
           .Returns(Task.CompletedTask);

        var sendHandler = CreateHandler(ctx, realOtpService, sms.Object, Mock.Of<INotificationService>());
        await sendHandler.Handle(new SendOtpCommand { MobileNumber = Mobile }, CancellationToken.None);

        Assert.NotNull(captured);

        var verifyHandler = new CmScheme.Registration.Application.Features.Registration.VerifyMobileOtp
            .VerifyMobileOtpCommandHandler(ctx, realOtpService);

        var verifyResult = await verifyHandler.Handle(
            new CmScheme.Registration.Application.Features.Registration.VerifyMobileOtp.VerifyMobileOtpCommand
            {
                MobileNumber = Mobile,
                OtpCode = captured!,
            },
            CancellationToken.None);

        Assert.True(verifyResult.IsSuccess);
    }
}
