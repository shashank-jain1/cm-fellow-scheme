using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;

namespace CmScheme.Registration.Application.Features.BulkImport.ImportUsers;

public sealed record ImportUsersCommand : ICommand<Result<int>>
{
    public Stream CsvStream { get; init; } = null!;
}
