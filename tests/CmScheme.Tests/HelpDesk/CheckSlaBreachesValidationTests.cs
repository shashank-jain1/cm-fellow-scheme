using CmScheme.HelpDesk.Application.Features.Sla.CheckSlaBreaches;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.HelpDesk;

public class CheckSlaBreachesValidationTests
{
    private readonly CheckSlaBreachesCommandValidator _validator = new();

    [Fact]
    public void Should_Be_Valid_For_Empty_Command()
    {
        // NOTE: CheckSlaBreachesCommandValidator defines no rules (empty constructor body),
        // so every command, including an empty one, must validate successfully.
        var command = new CheckSlaBreachesCommand();

        var result = _validator.TestValidate(command);

        Assert.True(result.IsValid);
    }
}