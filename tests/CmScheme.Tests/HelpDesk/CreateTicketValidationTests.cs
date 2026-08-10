using CmScheme.HelpDesk.Application.Features.Ticket.CreateTicket;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.HelpDesk;

public class CreateTicketValidationTests
{
    private readonly CreateTicketCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_ApplicantId_Is_Zero()
    {
        var command = new CreateTicketCommand { ApplicantId = 0 };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ApplicantId)
            .WithErrorMessage("ApplicantId must be greater than 0.");
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Empty()
    {
        var command = new CreateTicketCommand { Email = "" };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Email)
            .WithErrorMessage("Email is required.");
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Invalid()
    {
        var command = new CreateTicketCommand { Email = "not-an-email" };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Email)
            .WithErrorMessage("Email must be valid.");
    }

    [Fact]
    public void Should_Have_Error_When_Mobile_Is_Empty()
    {
        var command = new CreateTicketCommand { Mobile = "" };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Mobile)
            .WithErrorMessage("Mobile is required.");
    }

    [Fact]
    public void Should_Have_Error_When_IssueCategory_Is_Empty()
    {
        var command = new CreateTicketCommand { IssueCategory = "" };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.IssueCategory)
            .WithErrorMessage("IssueCategory is required.");
    }

    [Fact]
    public void Should_Have_Error_When_IssueDescription_Is_Empty()
    {
        var command = new CreateTicketCommand { IssueDescription = "" };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.IssueDescription)
            .WithErrorMessage("IssueDescription is required.");
    }

    [Fact]
    public void Should_Have_Error_When_IssueDescription_Exceeds_Maximum_Length()
    {
        var command = new CreateTicketCommand { IssueDescription = new string('a', 2001) };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.IssueDescription)
            .WithErrorMessage("IssueDescription must not exceed 2000 characters.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new CreateTicketCommand
        {
            ApplicantId = 1,
            Email = "fellow@test.gov.in",
            Mobile = "9876543210",
            IssueCategory = "Hardware",
            IssueDescription = "Laptop is not powering on.",
            Priority = "High",
            CategoryId = 3
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}