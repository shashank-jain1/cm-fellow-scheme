using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Registration.Core.Data;
using CmScheme.Registration.Core.Entities;

namespace CmScheme.Registration.Application.Features.Registration.VerifyMobileOtp;

public sealed class VerifyMobileOtpCommandHandler(IRegistrationCommandDbContext dbContext)
    : ICommandHandler<VerifyMobileOtpCommand, Result>
{
    public async ValueTask<Result> Handle(
        VerifyMobileOtpCommand request,
        CancellationToken cancellationToken)
    {
        Applicant? applicant = await dbContext.Applicants
            .FirstOrDefaultAsync(
                a => a.ApplicantId == request.ApplicantId && a.MobileNumber == request.MobileNumber,
                cancellationToken);

        if (applicant is null)
        {
            return Result.NotFound("Applicant not found.");
        }

        if (string.IsNullOrEmpty(request.OtpCode) || request.OtpCode.Length != 6)
        {
            return Result.Invalid(new ValidationError("Invalid OTP code. Must be 6 digits."));
        }

        applicant.ModifiedOn = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.NoContent();
    }
}
