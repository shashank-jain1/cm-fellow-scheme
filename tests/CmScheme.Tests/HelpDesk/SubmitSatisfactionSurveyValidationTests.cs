using CmScheme.HelpDesk.Application.Features.Ticket.SubmitSurvey;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.HelpDesk;

public class SubmitSatisfactionSurveyValidationTests
{
    private readonly SubmitSatisfactionSurveyCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_TicketId_Is_Zero()
    {
        var command = new SubmitSatisfactionSurveyCommand { TicketId = 0, UserAccountId = 1, Rating = 5 };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.TicketId)
            .WithErrorMessage("TicketId must be greater than 0.");
    }

    [Fact]
    public void Should_Have_Error_When_UserAccountId_Is_Zero()
    {
        var command = new SubmitSatisfactionSurveyCommand { TicketId = 1, UserAccountId = 0, Rating = 5 };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.UserAccountId)
            .WithErrorMessage("UserAccountId must be greater than 0.");
    }

    [Fact]
    public void Should_Have_Error_When_Rating_Is_Below_Minimum()
    {
        var command = new SubmitSatisfactionSurveyCommand { TicketId = 1, UserAccountId = 1, Rating = 0 };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Rating)
            .WithErrorMessage("Rating must be between 1 and 5.");
    }

    [Fact]
    public void Should_Have_Error_When_Rating_Is_Above_Maximum()
    {
        var command = new SubmitSatisfactionSurveyCommand { TicketId = 1, UserAccountId = 1, Rating = 6 };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Rating)
            .WithErrorMessage("Rating must be between 1 and 5.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Rating_Is_On_Boundary()
    {
        var command = new SubmitSatisfactionSurveyCommand { TicketId = 1, UserAccountId = 1, Rating = 1 };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(x => x.Rating);
    }

    [Fact]
    public void Should_Have_Error_When_Comments_Exceed_Maximum_Length()
    {
        var command = new SubmitSatisfactionSurveyCommand
        {
            TicketId = 1,
            UserAccountId = 1,
            Rating = 5,
            Comments = new string('a', 501)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Comments)
            .WithErrorMessage("Comments must not exceed 500 characters.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new SubmitSatisfactionSurveyCommand
        {
            TicketId = 1,
            UserAccountId = 1,
            Rating = 4,
            Comments = "Quick and helpful."
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}