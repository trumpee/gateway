using Trumpee.MassTransit.Messages.Notifications;

namespace Infrastructure.Persistence.MassTransit;

public interface INotificationsMassTransitClient :
    IMassTransitClient<Notification>
{
}