using CmScheme.Registration.Application.Features.Registration.SubmitRegistration;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.Registration;

public class SubmitRegistrationValidationTests
{
    private readonly SubmitRegistrationCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_Age_Under_18()
    {
        var command = new SubmitRegistrationCommand
        {
            DateOfBirth = DateTime.UtcNow.AddYears(-17),
            AadhaarNumber = "123456789012"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.DateOfBirth)
            .WithErrorMessage("Applicant must be at least 18 years old.");
    }

    [Fact]
    public void Should_Have_Error_When_No_Identity_Proof_Provided()
    {
        var command = new SubmitRegistrationCommand
        {
            DateOfBirth = DateTime.UtcNow.AddYears(-22),
            AadhaarNumber = null,
            PanNumber = null
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x)
            .WithErrorMessage("At least one identity proof (Aadhaar number or PAN number) must be provided.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Valid_Age_And_Aadhaar_Provided()
    {
        var command = new SubmitRegistrationCommand
        {
            FirstName = "Test",
            LastName = "Fellow",
            FatherName = "Father",
            EmailId = "fellow@test.gov.in",
            MobileNumber = "9876543210",
            DateOfBirth = DateTime.UtcNow.AddYears(-22),
            AadhaarNumber = "123456789012"
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(x => x.DateOfBirth);
    }
}
