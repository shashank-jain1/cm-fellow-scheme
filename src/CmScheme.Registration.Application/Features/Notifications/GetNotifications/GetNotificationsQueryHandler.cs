using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Registration.Core.Data;

namespace CmScheme.Registration.Application.Features.Notifications.GetNotifications;

public sealed class GetNotificationsQueryHandler(IRegistrationQueryDbContext dbContext)
    : IQueryHandler<GetNotificationsQuery, Result<IReadOnlyList<NotificationListItem>>>
{
    public async ValueTask<Result<IReadOnlyList<NotificationListItem>>> Handle(
        GetNotificationsQuery request,
        CancellationToken cancellationToken)
    {
        IReadOnlyList<NotificationListItem> result = await dbContext.Notifications
            .Where(n => n.UserId == request.UserId || n.UserId == null)
            .OrderByDescending(n => n.SentOn)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(n => new NotificationListItem
            {
                NotificationId = n.NotificationId,
                Channel = n.Channel,
                Subject = n.Subject,
                Body = n.Body,
                Status = n.Status,
                SentOn = n.SentOn,
            })
            .ToListAsync(cancellationToken);

        return Result<IReadOnlyList<NotificationListItem>>.Success(result);
    }
}
