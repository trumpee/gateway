namespace Infrastructure.Persistence.MassTransit.Analytics;

public interface INotificationsAnalyticsClient
{
    Task SendNotificationCreated(string notificationId, CancellationToken ct);
    Task SendNotificationReceived(string notificationId, CancellationToken ct);
}