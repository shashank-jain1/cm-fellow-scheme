using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Common.Core.Services;
using CmScheme.Registration.Core.Data;
using CmScheme.Registration.Core.Entities;

namespace CmScheme.Registration.Application.Features.Registration.SendOtp;

public sealed class SendOtpCommandHandler(
    IRegistrationCommandDbContext dbContext,
    IOtpService otpService,
    INotificationService notificationService)
    : ICommandHandler<SendOtpCommand, Result>
{
    public async ValueTask<Result> Handle(
        SendOtpCommand request,
        CancellationToken cancellationToken)
    {
        Applicant? applicant = await dbContext.Applicants
            .FirstOrDefaultAsync(
                a => a.ApplicantId == request.ApplicantId && a.EmailId == request.EmailId,
                cancellationToken);

        if (applicant is null)
        {
            return Result.NotFound("Applicant not found.");
        }

        string otp = await otpService.GenerateOtpAsync(request.EmailId, cancellationToken);

        await notificationService.SendEmailAsync(
            request.EmailId,
            "Your OTP for CM Fellow Registration",
            $"Dear {applicant.FirstName} {applicant.LastName},\n\n" +
            $"Your One-Time Password (OTP) for registration verification is:\n\n" +
            $"OTP: {otp}\n\n" +
            $"This OTP is valid for 5 minutes. Do not share it with anyone.\n\n" +
            $"Best regards,\nCM Fellow Program Team",
            cancellationToken);

        return Result.NoContent();
    }
}
