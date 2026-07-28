using Ardalis.Result;
using Mediator;

namespace CmScheme.Certificate.Application.Features.Exit.CloseAndArchiveRecord;

public sealed record CloseAndArchiveRecordCommand : ICommand<Result>
{
    public int ExitRecordId { get; init; }
    public int ApprovedBy { get; init; }
}
