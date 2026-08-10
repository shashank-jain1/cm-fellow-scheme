using Ardalis.Result;
using CmScheme.Registration.Application.Features.Registration.GetRegistrationById;
using CmScheme.Registration.Core.Entities;
using CmScheme.Registration.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Tests.Registration.Handlers;

public class GetRegistrationByIdQueryHandlerTests
{
    private static GetRegistrationByIdQueryHandler CreateHandler(RegistrationQueryDbContext ctx) => new(ctx);

    private static Applicant BuildApplicant() => new()
    {
        FirstName = "Rohit",
        MiddleName = "K",
        LastName = "Sharma",
        FatherName = "Kishan",
        MobileNumber = "9876543210",
        EmailId = "rohit@example.com",
        DateOfBirth = new DateTime(2000, 1, 1),
        PermanentAddress = "Mumbai",
        PinCode = "400001",
        BoardUniversityName = "MU",
        Status = "Pending"
    };

    [Fact]
    public async Task Handle_Existing_Applicant_Returns_Dto_With_FullName()
    {
        var ctx = TestDbContext.CreateRegistrationQuery(c =>
        {
            c.Applicants.Add(BuildApplicant());
            c.SaveChanges();
        });
        var handler = CreateHandler(ctx);
        var applicantId = await ctx.Applicants.Select(a => a.ApplicantId).FirstAsync();

        var result = await handler.Handle(new GetRegistrationByIdQuery { ApplicantId = applicantId }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(applicantId, result.Value.ApplicantId);
        Assert.Equal("Rohit", result.Value.FirstName);
        Assert.Equal("rohit@example.com", result.Value.EmailId);
        Assert.Equal("Rohit K Sharma", result.Value.FullName);
    }

    [Fact]
    public async Task Handle_No_MiddleName_FullName_Has_Single_Space()
    {
        var ctx = TestDbContext.CreateRegistrationQuery(c =>
        {
            var applicant = BuildApplicant();
            applicant.MiddleName = null;
            c.Applicants.Add(applicant);
            c.SaveChanges();
        });
        var handler = CreateHandler(ctx);
        var applicantId = await ctx.Applicants.Select(a => a.ApplicantId).FirstAsync();

        var result = await handler.Handle(new GetRegistrationByIdQuery { ApplicantId = applicantId }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal("Rohit Sharma", result.Value.FullName);
    }

    [Fact]
    public async Task Handle_Missing_Applicant_Returns_NotFound()
    {
        var ctx = TestDbContext.CreateRegistrationQuery();
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new GetRegistrationByIdQuery { ApplicantId = 999 }, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.NotFound, result.Status);
        Assert.Contains("Applicant not found.", result.Errors);
    }
}