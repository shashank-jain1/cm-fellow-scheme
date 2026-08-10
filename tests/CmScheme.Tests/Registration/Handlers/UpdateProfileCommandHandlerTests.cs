using Ardalis.Result;
using CmScheme.Registration.Application.Features.Registration.UpdateProfile;
using CmScheme.Registration.Core.Entities;
using CmScheme.Registration.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Tests.Registration.Handlers;

public class UpdateProfileCommandHandlerTests
{
    private static UpdateProfileCommandHandler CreateHandler(RegistrationCommandDbContext ctx) => new(ctx);

    private static Applicant BuildApplicant() => new()
    {
        FirstName = "Old",
        LastName = "Name",
        FatherName = "Father",
        MobileNumber = "9876543210",
        EmailId = "old@example.com",
        PermanentAddress = "Old Address",
        PinCode = "400001",
        BoardUniversityName = "MU",
        Status = "Pending"
    };

    [Fact]
    public async Task Handle_Existing_Applicant_Updates_Profile()
    {
        var ctx = TestDbContext.CreateRegistration(c =>
        {
            c.Applicants.Add(BuildApplicant());
            c.SaveChanges();
        });
        var handler = CreateHandler(ctx);
        var applicantId = await ctx.Applicants.Select(a => a.ApplicantId).FirstAsync();

        var result = await handler.Handle(new UpdateProfileCommand
        {
            ApplicantId = applicantId,
            FirstName = "New",
            MiddleName = "M",
            LastName = "Surname",
            MobileNumber = "1111111111",
            EmailId = "new@example.com",
            PermanentAddress = "New Address",
            QualificationId = 5,
            ExperienceDetails = "5 yrs"
        }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(ResultStatus.NoContent, result.Status);
        var updated = await ctx.Applicants.FirstAsync();
        Assert.Equal("New", updated.FirstName);
        Assert.Equal("Surname", updated.LastName);
        Assert.Equal("1111111111", updated.MobileNumber);
        Assert.Equal("new@example.com", updated.EmailId);
        Assert.Equal("New Address", updated.PermanentAddress);
        Assert.Equal(5, updated.QualificationId);
        Assert.Equal("5 yrs", updated.ExperienceDetails);
    }

    [Fact]
    public async Task Handle_Missing_Applicant_Returns_NotFound()
    {
        var ctx = TestDbContext.CreateRegistration();
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new UpdateProfileCommand
        {
            ApplicantId = 999,
            FirstName = "X",
            LastName = "Y",
            MobileNumber = "1000000000",
            EmailId = "x@example.com",
            PermanentAddress = "There"
        }, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.NotFound, result.Status);
        Assert.Contains("Applicant not found.", result.Errors);
    }
}