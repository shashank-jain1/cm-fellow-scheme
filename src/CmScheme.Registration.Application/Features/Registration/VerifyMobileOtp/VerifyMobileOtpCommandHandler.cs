using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Common.Core.Services;
using CmScheme.Registration.Core.Data;
using CmScheme.Registration.Core.Entities;

namespace CmScheme.Registration.Application.Features.Registration.VerifyMobileOtp;

public sealed class VerifyMobileOtpCommandHandler(
    IRegistrationCommandDbContext dbContext,
    IOtpService otpService)
    : ICommandHandler<VerifyMobileOtpCommand, Result>
{
    public async ValueTask<Result> Handle(
        VerifyMobileOtpCommand request,
        CancellationToken cancellationToken)
    {
        bool isValid = await otpService.VerifyOtpAsync(request.MobileNumber, request.OtpCode, cancellationToken);

        if (!isValid)
        {
            return Result.Invalid(new ValidationError("Invalid or expired OTP code."));
        }

        // When the mobile already belongs to an applicant (re-verification), record the touch.
        Applicant? applicant = await dbContext.Applicants
            .FirstOrDefaultAsync(a => a.MobileNumber == request.MobileNumber, cancellationToken);

        if (applicant is not null)
        {
            applicant.ModifiedOn = DateTime.UtcNow;
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        return Result.NoContent();
    }
}
