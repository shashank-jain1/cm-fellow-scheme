using CmScheme.Registration.Application.Features.BulkImport.ImportUsers;
using FluentValidation.TestHelper;

namespace CmScheme.Tests.Registration;

public class ImportUsersCommandValidatorTests
{
    private readonly ImportUsersCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_CsvStream_Is_Null()
    {
        var command = new ImportUsersCommand
        {
            CsvStream = null!
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.CsvStream)
            .WithErrorMessage("CSV file stream is required.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_CsvStream_Is_Provided()
    {
        var command = new ImportUsersCommand
        {
            CsvStream = new MemoryStream()
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(x => x.CsvStream);
    }

    [Fact]
    public void Should_Be_Valid_When_CsvStream_Is_Provided()
    {
        var command = new ImportUsersCommand
        {
            CsvStream = new MemoryStream()
        };

        var result = _validator.TestValidate(command);
        Assert.True(result.IsValid);
    }
}