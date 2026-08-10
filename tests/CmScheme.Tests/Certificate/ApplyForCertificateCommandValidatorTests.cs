using CmScheme.Certificate.Application.Features.Certificate.ApplyForCertificate;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.Certificate;

public class ApplyForCertificateCommandValidatorTests
{
    private readonly ApplyForCertificateCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_ApplicantId_Is_Zero()
    {
        var command = new ApplyForCertificateCommand
        {
            ApplicantId = 0,
            ApplicantName = "John Doe",
            ProgramName = "CM Fellowship",
            StartDate = new DateTime(2026, 1, 1),
            EndDate = new DateTime(2026, 6, 30),
            DurationDays = 180,
            CreatedBy = "Admin"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ApplicantId)
            .WithErrorMessage("ApplicantId must be greater than 0.");
    }

    [Fact]
    public void Should_Have_Error_When_ApplicantName_Is_Empty()
    {
        var command = new ApplyForCertificateCommand
        {
            ApplicantId = 1,
            ApplicantName = "",
            ProgramName = "CM Fellowship",
            StartDate = new DateTime(2026, 1, 1),
            EndDate = new DateTime(2026, 6, 30),
            DurationDays = 180,
            CreatedBy = "Admin"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ApplicantName)
            .WithErrorMessage("ApplicantName is required.");
    }

    [Fact]
    public void Should_Have_Error_When_ApplicantName_Exceeds_MaxLength()
    {
        var command = new ApplyForCertificateCommand
        {
            ApplicantId = 1,
            ApplicantName = new string('n', 201),
            ProgramName = "CM Fellowship",
            StartDate = new DateTime(2026, 1, 1),
            EndDate = new DateTime(2026, 6, 30),
            DurationDays = 180,
            CreatedBy = "Admin"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ApplicantName)
            .WithErrorMessage("ApplicantName must not exceed 200 characters.");
    }

    [Fact]
    public void Should_Have_Error_When_ProgramName_Is_Empty()
    {
        var command = new ApplyForCertificateCommand
        {
            ApplicantId = 1,
            ApplicantName = "John Doe",
            ProgramName = "",
            StartDate = new DateTime(2026, 1, 1),
            EndDate = new DateTime(2026, 6, 30),
            DurationDays = 180,
            CreatedBy = "Admin"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ProgramName)
            .WithErrorMessage("ProgramName is required.");
    }

    [Fact]
    public void Should_Have_Error_When_EndDate_Is_Before_StartDate()
    {
        var command = new ApplyForCertificateCommand
        {
            ApplicantId = 1,
            ApplicantName = "John Doe",
            ProgramName = "CM Fellowship",
            StartDate = new DateTime(2026, 6, 30),
            EndDate = new DateTime(2026, 1, 1),
            DurationDays = 180,
            CreatedBy = "Admin"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.EndDate)
            .WithErrorMessage("EndDate must be on or after StartDate.");
    }

    [Fact]
    public void Should_Have_Error_When_DurationDays_Is_Zero()
    {
        var command = new ApplyForCertificateCommand
        {
            ApplicantId = 1,
            ApplicantName = "John Doe",
            ProgramName = "CM Fellowship",
            StartDate = new DateTime(2026, 1, 1),
            EndDate = new DateTime(2026, 6, 30),
            DurationDays = 0,
            CreatedBy = "Admin"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.DurationDays)
            .WithErrorMessage("DurationDays must be greater than 0.");
    }

    [Fact]
    public void Should_Have_Error_When_CreatedBy_Is_Empty()
    {
        var command = new ApplyForCertificateCommand
        {
            ApplicantId = 1,
            ApplicantName = "John Doe",
            ProgramName = "CM Fellowship",
            StartDate = new DateTime(2026, 1, 1),
            EndDate = new DateTime(2026, 6, 30),
            DurationDays = 180,
            CreatedBy = ""
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.CreatedBy)
            .WithErrorMessage("CreatedBy is required.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new ApplyForCertificateCommand
        {
            ApplicantId = 1,
            ApplicantName = "John Doe",
            ProgramName = "CM Fellowship",
            StartDate = new DateTime(2026, 1, 1),
            EndDate = new DateTime(2026, 6, 30),
            DurationDays = 180,
            CreatedBy = "Admin"
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}