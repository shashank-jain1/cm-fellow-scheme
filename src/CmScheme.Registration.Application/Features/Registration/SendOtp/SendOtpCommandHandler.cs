using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CmScheme.Common.Core.Services;
using CmScheme.Registration.Core.Data;
using CmScheme.Registration.Core.Entities;

namespace CmScheme.Registration.Application.Features.Registration.SendOtp;

public sealed class SendOtpCommandHandler(
    IRegistrationCommandDbContext dbContext,
    IOtpService otpService,
    ISmsService smsService,
    INotificationService notificationService,
    ILogger<SendOtpCommandHandler> logger)
    : ICommandHandler<SendOtpCommand, Result>
{
    public async ValueTask<Result> Handle(
        SendOtpCommand request,
        CancellationToken cancellationToken)
    {
        string otp = await otpService.GenerateOtpAsync(request.MobileNumber, cancellationToken);

        // SMS is the channel the spec designates for mobile verification.
        await smsService.SendSmsAsync(
            request.MobileNumber,
            $"Your OTP for CM Fellow registration is {otp}. It is valid for 5 minutes. Do not share it with anyone.",
            cancellationToken);

        // If this mobile already belongs to an applicant, copy the OTP to their email too.
        Applicant? applicant = await dbContext.Applicants
            .FirstOrDefaultAsync(a => a.MobileNumber == request.MobileNumber, cancellationToken);

        if (applicant is not null && !string.IsNullOrWhiteSpace(applicant.EmailId))
        {
            try
            {
                await notificationService.SendEmailAsync(
                    applicant.EmailId,
                    "Your OTP for CM Fellow Registration",
                    $"Dear {applicant.FirstName} {applicant.LastName},\n\n" +
                    $"Your One-Time Password (OTP) for registration verification is:\n\n" +
                    $"OTP: {otp}\n\n" +
                    $"This OTP is valid for 5 minutes. Do not share it with anyone.\n\n" +
                    $"Best regards,\nCM Fellow Program Team",
                    cancellationToken);
            }
            catch (Exception ex)
            {
                // The SMS already went out; a failing mail relay must not fail the request.
                logger.LogWarning(ex, "Could not email the OTP copy to {Email}.", applicant.EmailId);
            }
        }

        return Result.NoContent();
    }
}
