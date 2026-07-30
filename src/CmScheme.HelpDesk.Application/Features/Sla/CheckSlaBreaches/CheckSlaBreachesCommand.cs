using Ardalis.Result;
using Mediator;

namespace CmScheme.HelpDesk.Application.Features.Sla.CheckSlaBreaches;

public sealed record CheckSlaBreachesCommand : ICommand<Result<int>>
{
}
