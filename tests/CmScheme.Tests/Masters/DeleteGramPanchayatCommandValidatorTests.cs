using CmScheme.Masters.Application.Features.Location.GramPanchayats.DeleteGramPanchayat;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.Masters;

public class DeleteGramPanchayatCommandValidatorTests
{
    private readonly DeleteGramPanchayatCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_GramPanchayatId_Is_Zero()
    {
        var command = new DeleteGramPanchayatCommand
        {
            GramPanchayatId = 0
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.GramPanchayatId)
            .WithErrorMessage("Valid gram panchayat is required.");
    }

    [Fact]
    public void Should_Have_Error_When_GramPanchayatId_Is_Negative()
    {
        var command = new DeleteGramPanchayatCommand
        {
            GramPanchayatId = -1
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.GramPanchayatId)
            .WithErrorMessage("Valid gram panchayat is required.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_GramPanchayatId_Is_Positive()
    {
        var command = new DeleteGramPanchayatCommand
        {
            GramPanchayatId = 1
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(x => x.GramPanchayatId);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new DeleteGramPanchayatCommand
        {
            GramPanchayatId = 1
        };

        var result = _validator.TestValidate(command);
        Assert.True(result.IsValid);
    }
}