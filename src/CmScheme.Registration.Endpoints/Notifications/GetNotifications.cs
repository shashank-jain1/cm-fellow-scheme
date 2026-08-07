using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Registration.Application.Features.Notifications.GetNotifications;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Registration.Endpoints.Notifications;

public sealed class GetNotifications
{
    public static async Task<IResult> Handle(
        int? userId,
        int? page,
        int? pageSize,
        ISender sender,
        CancellationToken cancellationToken)
    {
        ValueTask<Result<IReadOnlyList<NotificationListItem>>> result = sender.Send(
            new GetNotificationsQuery
            {
                UserId = userId ?? 0,
                Page = page ?? 1,
                PageSize = pageSize ?? 20,
            },
            cancellationToken);

        return await result.ToApiResultAsync();
    }
}
