using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Certificate.Application.Features.Exit.CloseAndArchiveRecord;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Certificate.Endpoints.Exit;

public sealed class CloseArchive
{
    public static async Task<IResult> Handle(int exitRecordId, CloseArchiveRequest request, ISender sender, CancellationToken ct)
    {
        CloseAndArchiveRecordCommand command = new CloseAndArchiveRecordCommand
        {
            ExitRecordId = exitRecordId,
            ApprovedBy = request.ApprovedBy
        };
        ValueTask<Result> result = sender.Send(command, ct);
        return await result.ToApiResultAsync();
    }
}

public sealed record CloseArchiveRequest(int ApprovedBy);
