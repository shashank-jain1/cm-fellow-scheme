using Ardalis.Result;
using Mediator;

namespace CmScheme.Registration.Application.Features.Notifications.GetNotifications;

public sealed record GetNotificationsQuery : IQuery<Result<IReadOnlyList<NotificationListItem>>>
{
    public int UserId { get; init; }
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 20;
}

public sealed record NotificationListItem
{
    public int NotificationId { get; init; }
    public string Channel { get; init; } = null!;
    public string Subject { get; init; } = null!;
    public string Body { get; init; } = null!;
    public string Status { get; init; } = null!;
    public DateTime SentOn { get; init; }
}
