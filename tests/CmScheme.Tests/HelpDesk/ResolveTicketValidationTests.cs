using CmScheme.HelpDesk.Application.Features.Ticket.ResolveTicket;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.HelpDesk;

public class ResolveTicketValidationTests
{
    private readonly ResolveTicketCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_TicketId_Is_Zero()
    {
        var command = new ResolveTicketCommand { TicketId = 0 };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.TicketId)
            .WithErrorMessage("TicketId must be greater than 0.");
    }

    [Fact]
    public void Should_Have_Error_When_ActionBy_Is_Empty()
    {
        var command = new ResolveTicketCommand { ActionBy = "" };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ActionBy)
            .WithErrorMessage("ActionBy is required.");
    }

    [Fact]
    public void Should_Have_Error_When_ActionBy_Exceeds_Maximum_Length()
    {
        var command = new ResolveTicketCommand { ActionBy = new string('a', 201) };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ActionBy)
            .WithErrorMessage("ActionBy must not exceed 200 characters.");
    }

    [Fact]
    public void Should_Have_Error_When_ResolutionRemarks_Is_Empty()
    {
        var command = new ResolveTicketCommand { ResolutionRemarks = "" };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ResolutionRemarks)
            .WithErrorMessage("ResolutionRemarks is required.");
    }

    [Fact]
    public void Should_Have_Error_When_ResolutionRemarks_Exceeds_Maximum_Length()
    {
        var command = new ResolveTicketCommand { ResolutionRemarks = new string('a', 2001) };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ResolutionRemarks)
            .WithErrorMessage("ResolutionRemarks must not exceed 2000 characters.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new ResolveTicketCommand
        {
            TicketId = 1,
            ActionBy = "support",
            ResolutionRemarks = "Replaced the battery and verified startup."
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}