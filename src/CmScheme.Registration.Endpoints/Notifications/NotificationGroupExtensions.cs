using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using CmScheme.Endpoints.Abstractions.Authorization;

namespace CmScheme.Registration.Endpoints.Notifications;

public static class NotificationGroupExtensions
{
    public static IEndpointRouteBuilder MapNotificationEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("/notifications")
            .WithTags("Notifications")
            .RequireAuthorization()
            .RequireModule(ModuleCodes.Registration, "Read", requireScope: false);

        group.MapGet("/", GetNotifications.Handle)
            .WithName("GetNotifications")
            .WithDisplayName("Get notifications for user");

        return group;
    }
}
