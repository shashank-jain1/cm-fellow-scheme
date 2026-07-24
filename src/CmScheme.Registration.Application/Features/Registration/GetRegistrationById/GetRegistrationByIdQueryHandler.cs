using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Registration.Core.Data;
using CmScheme.Registration.Core.Dtos;

namespace CmScheme.Registration.Application.Features.Registration.GetRegistrationById;

public sealed class GetRegistrationByIdQueryHandler(IRegistrationQueryDbContext dbContext)
    : IQueryHandler<GetRegistrationByIdQuery, Result<ApplicantDto>>
{
    public async ValueTask<Result<ApplicantDto>> Handle(
        GetRegistrationByIdQuery request,
        CancellationToken cancellationToken)
    {
        ApplicantDto? dto = await dbContext.Applicants
            .Where(a => a.ApplicantId == request.ApplicantId)
            .Select(a => new ApplicantDto(
                a.ApplicantId,
                a.FirstName,
                a.MiddleName,
                a.LastName,
                a.FatherName,
                a.MobileNumber,
                a.EmailId,
                a.DateOfBirth,
                a.Status,
                a.FirstName + " " + (a.MiddleName != null ? a.MiddleName + " " : "") + a.LastName))
            .FirstOrDefaultAsync(cancellationToken);

        if (dto is null)
        {
            return Result<ApplicantDto>.NotFound("Applicant not found.");
        }

        return Result<ApplicantDto>.Success(dto);
    }
}
