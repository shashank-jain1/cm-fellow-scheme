using CmScheme.Masters.Application.Features.Location.GramPanchayats.UpdateGramPanchayat;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.Masters;

public class UpdateGramPanchayatCommandValidatorTests
{
    private readonly UpdateGramPanchayatCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_GramPanchayatId_Is_Zero()
    {
        var command = new UpdateGramPanchayatCommand
        {
            GramPanchayatId = 0,
            BlockId = 1,
            GramPanchayatName = "Kolar Gram Panchayat"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.GramPanchayatId)
            .WithErrorMessage("Valid gram panchayat is required.");
    }

    [Fact]
    public void Should_Have_Error_When_BlockId_Is_Zero()
    {
        var command = new UpdateGramPanchayatCommand
        {
            GramPanchayatId = 1,
            BlockId = 0,
            GramPanchayatName = "Kolar Gram Panchayat"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.BlockId)
            .WithErrorMessage("Valid block is required.");
    }

    [Fact]
    public void Should_Have_Error_When_GramPanchayatName_Is_Empty()
    {
        var command = new UpdateGramPanchayatCommand
        {
            GramPanchayatId = 1,
            BlockId = 1,
            GramPanchayatName = ""
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.GramPanchayatName)
            .WithErrorMessage("Gram Panchayat name is required.");
    }

    [Fact]
    public void Should_Have_Error_When_GramPanchayatName_Exceeds_MaximumLength()
    {
        var command = new UpdateGramPanchayatCommand
        {
            GramPanchayatId = 1,
            BlockId = 1,
            GramPanchayatName = new string('G', 151)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.GramPanchayatName)
            .WithErrorMessage("Gram Panchayat name must not exceed 150 characters.");
    }

    [Fact]
    public void Should_Have_Error_When_GPCode_Exceeds_MaximumLength()
    {
        var command = new UpdateGramPanchayatCommand
        {
            GramPanchayatId = 1,
            BlockId = 1,
            GramPanchayatName = "Kolar Gram Panchayat",
            GPCode = new string('X', 21)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.GPCode)
            .WithErrorMessage("GP code must not exceed 20 characters.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new UpdateGramPanchayatCommand
        {
            GramPanchayatId = 1,
            BlockId = 1,
            GramPanchayatName = "Kolar Gram Panchayat",
            GPCode = "KL"
        };

        var result = _validator.TestValidate(command);
        Assert.True(result.IsValid);
    }
}