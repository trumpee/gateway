using MassTransit;
using Trumpee.MassTransit;
using Trumpee.MassTransit.Messages.Analytics.Notifications;
using Trumpee.MassTransit.Messages.Analytics.Notifications.Payloads;

namespace Infrastructure.Persistence.MassTransit.Analytics;

public class NotificationsAnalyticsClient(
    ISendEndpointProvider endpointProvider) : INotificationsAnalyticsClient
{
    private const string EventSource = "Trumpee Gateway";
    
    private readonly ISendEndpointProvider _endpointProvider = endpointProvider;
    
    private static readonly string NotificationCreated =
        QueueNames.Analytics.Notifications(typeof(NotificationCreatedPayload));
    
    public Task SendNotificationCreated(string notificationId, CancellationToken ct)
    {
        var analyticsEvent = Notification.Created(EventSource, notificationId);
        return SendEvent(analyticsEvent, NotificationCreated, ct);
    }
    
    public Task SendNotificationReceived(string notificationId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
    
    private async Task SendEvent<T>(T analyticsEvent, string queueName, CancellationToken ct) where T : class
    {
        var endpoint = await _endpointProvider.GetSendEndpoint(new Uri(queueName));
        await endpoint.Send(analyticsEvent, ct);
    }
}