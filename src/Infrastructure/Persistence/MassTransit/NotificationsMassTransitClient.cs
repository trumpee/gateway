using MassTransit;
using Trumpee.MassTransit.Messages.Notifications;

namespace Infrastructure.Persistence.MassTransit;

public class NotificationsMassTransitClient :
    MassTransitClient<Notification>, INotificationsMassTransitClient
{
    public NotificationsMassTransitClient(ISendEndpointProvider endpointProvider) :
        base(endpointProvider)
    {
    }
}