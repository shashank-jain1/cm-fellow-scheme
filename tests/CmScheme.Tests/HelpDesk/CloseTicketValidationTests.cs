using CmScheme.HelpDesk.Application.Features.Ticket.CloseTicket;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.HelpDesk;

public class CloseTicketValidationTests
{
    private readonly CloseTicketCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_TicketId_Is_Zero()
    {
        var command = new CloseTicketCommand { TicketId = 0 };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.TicketId)
            .WithErrorMessage("TicketId must be greater than 0.");
    }

    [Fact]
    public void Should_Have_Error_When_ActionBy_Is_Empty()
    {
        var command = new CloseTicketCommand { ActionBy = "" };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ActionBy)
            .WithErrorMessage("ActionBy is required.");
    }

    [Fact]
    public void Should_Have_Error_When_ActionBy_Exceeds_Maximum_Length()
    {
        var command = new CloseTicketCommand { ActionBy = new string('a', 201) };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ActionBy)
            .WithErrorMessage("ActionBy must not exceed 200 characters.");
    }

    [Fact]
    public void Should_Have_Error_When_Remarks_Is_Empty()
    {
        var command = new CloseTicketCommand { Remarks = "" };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Remarks)
            .WithErrorMessage("Remarks is required.");
    }

    [Fact]
    public void Should_Have_Error_When_Remarks_Exceeds_Maximum_Length()
    {
        var command = new CloseTicketCommand { Remarks = new string('a', 2001) };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Remarks)
            .WithErrorMessage("Remarks must not exceed 2000 characters.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new CloseTicketCommand
        {
            TicketId = 1,
            ActionBy = "admin",
            Remarks = "Issue resolved and ticket closed."
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}