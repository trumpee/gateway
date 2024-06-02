using Core.Models.Notifications;
using Trumpee.MassTransit.Messages.Notifications;
using Priority = Trumpee.MassTransit.Messages.Notifications.Priority;

namespace Core.Mappers.External;

internal static class DeliveryRequestMapper
{
    internal static Notification ToRequest(NotificationDto notification, RecipientDto recipient)
    {
        ArgumentNullException.ThrowIfNull(notification.Content);

        return new Notification
        {
            NotificationId = notification.Id!,
            Content = ContentNapper.ToContent(notification.Content),
            Priority = (Priority?)notification.Priority,
            Status = notification.Status,
            Timestamp = notification.DeliveryTimestamp,
            Recipient = RecipientMapper.ToRequest(recipient)
        };
    }
}