using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using CmScheme.Registration.Application.Features.BulkImport.ImportUsers;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Registration.Endpoints.BulkImport;

public sealed class ImportUsers
{
    public static async Task<IResult> Handle(
        IFormFile file,
        ISender sender,
        CancellationToken cancellationToken)
    {
        await using Stream stream = file.OpenReadStream();

        ImportUsersCommand command = new ImportUsersCommand
        {
            CsvStream = stream
        };

        ValueTask<Result<int>> result = sender.Send(command, cancellationToken);
        return await result.ToApiResultAsync();
    }
}
