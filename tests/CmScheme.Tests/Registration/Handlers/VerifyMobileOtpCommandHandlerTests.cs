using Ardalis.Result;
using CmScheme.Common.Core.Services;
using CmScheme.Registration.Application.Features.Registration.VerifyMobileOtp;
using CmScheme.Registration.Core.Entities;
using CmScheme.Registration.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CmScheme.Tests.Registration.Handlers;

public class VerifyMobileOtpCommandHandlerTests
{
    private const string Mobile = "9876543210";

    private static VerifyMobileOtpCommandHandler CreateHandler(
        RegistrationCommandDbContext ctx,
        IOtpService otpService) => new(ctx, otpService);

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
    public async Task Handle_Valid_Otp_Verifies_Against_The_Mobile_Number()
    {
        var ctx = TestDbContext.CreateRegistration(c =>
        {
            c.Applicants.Add(BuildApplicant());
            c.SaveChanges();
        });
        var otp = new Mock<IOtpService>();
        otp.Setup(o => o.VerifyOtpAsync(Mobile, "123456", It.IsAny<CancellationToken>())).ReturnsAsync(true);
        var handler = CreateHandler(ctx, otp.Object);

        var result = await handler.Handle(
            new VerifyMobileOtpCommand { MobileNumber = Mobile, OtpCode = "123456" },
            CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(ResultStatus.NoContent, result.Status);
        otp.Verify(o => o.VerifyOtpAsync(Mobile, "123456", It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Valid_Otp_Succeeds_Before_The_Applicant_Exists()
    {
        var ctx = TestDbContext.CreateRegistration();
        var otp = new Mock<IOtpService>();
        otp.Setup(o => o.VerifyOtpAsync(Mobile, "123456", It.IsAny<CancellationToken>())).ReturnsAsync(true);
        var handler = CreateHandler(ctx, otp.Object);

        var result = await handler.Handle(
            new VerifyMobileOtpCommand { MobileNumber = Mobile, OtpCode = "123456" },
            CancellationToken.None);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task Handle_Valid_Otp_Stamps_An_Existing_Applicant()
    {
        var ctx = TestDbContext.CreateRegistration(c =>
        {
            c.Applicants.Add(BuildApplicant());
            c.SaveChanges();
        });
        var otp = new Mock<IOtpService>();
        otp.Setup(o => o.VerifyOtpAsync(Mobile, "123456", It.IsAny<CancellationToken>())).ReturnsAsync(true);
        var handler = CreateHandler(ctx, otp.Object);

        await handler.Handle(
            new VerifyMobileOtpCommand { MobileNumber = Mobile, OtpCode = "123456" },
            CancellationToken.None);

        var applicant = await ctx.Applicants.FirstAsync();
        Assert.True(applicant.ModifiedOn <= DateTime.UtcNow);
    }

    [Fact]
    public async Task Handle_Wrong_Otp_Returns_Invalid()
    {
        var ctx = TestDbContext.CreateRegistration(c =>
        {
            c.Applicants.Add(BuildApplicant());
            c.SaveChanges();
        });
        var otp = new Mock<IOtpService>();
        otp.Setup(o => o.VerifyOtpAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(false);
        var handler = CreateHandler(ctx, otp.Object);

        var result = await handler.Handle(
            new VerifyMobileOtpCommand { MobileNumber = Mobile, OtpCode = "000000" },
            CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.Invalid, result.Status);
        Assert.Contains("Invalid or expired OTP code.", result.ValidationErrors.Select(e => e.ErrorMessage));
    }
}
