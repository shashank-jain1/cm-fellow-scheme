using Ardalis.Result;
using CmScheme.Masters.Application.Features.LookupMaster.CreateLookupMaster;
using Mediator;
using Microsoft.AspNetCore.Http;

namespace CmScheme.Masters.Endpoints.LookupMasters;

public static class Create
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Handle(
        CreateLookupMasterCommand command,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        Result<int> result = await mediator.Send(command, cancellationToken);

        return result.IsSuccess
            ? Results.Created($"/masters/lookup/{result.Value}", result.Value)
            : Results.BadRequest(result.Errors);
    }
}
