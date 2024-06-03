using Core.Abstractions;
using Core.Mappers;
using Core.Models.Notifications;
using ErrorOr;
using Infrastructure.Persistence.MassTransit;
using Infrastructure.Persistence.MassTransit.Analytics;
using Infrastructure.Persistence.Mongo.Abstractions;
using Trumpee.MassTransit.Messages.Notifications;

namespace Core.Services;

internal class NotificationService(
        ITemplateFillerClient massTransitClient,
        IUserPreferencesService userPreferencesService,
        INotificationsAnalyticsClient notificationsAnalyticsClient,
        INotificationsRepository notificationsRepository)
    : INotificationsService
{
    public async Task<ErrorOr<NotificationDto>> CreateNotification(
        NotificationDto dto, CancellationToken ct)
    {
        var notification = NotificationMapper.ToEntity(dto);

        ct.ThrowIfCancellationRequested();
        await notificationsRepository.InsertOne(notification);
        dto = dto with { Id = notification.Id.ToString() };

        var deliveryRequests = await CreateDeliveryRequests(dto, ct);

        await massTransitClient.SendMessages(deliveryRequests, string.Empty);
        foreach (var deliveryRequest in deliveryRequests)
        {
            await notificationsAnalyticsClient
                .SendNotificationCreated(deliveryRequest.NotificationId, ct);
        }

        return dto;
    }

    private async Task<List<Notification>> CreateDeliveryRequests(NotificationDto dto, CancellationToken ct)
    {
        return await dto.Recipients!
            .ToAsyncEnumerable()
            .Select(recipient => Mappers.External.DeliveryRequestMapper.ToRequest(dto, recipient))
            .SelectAwait(deliveryRequest => PopulateDeliveryInfo(deliveryRequest, ct))
            .ToListAsync(cancellationToken: ct);
    }

    private async ValueTask<Notification> PopulateDeliveryInfo(Notification notification, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();

        var channel = notification.Recipient.Channel;
        var deliveryInfo = await userPreferencesService
            .GetChannelDeliveryInfo(notification.Recipient.UserId, channel, ct);
        if (deliveryInfo.IsError)
        {
            return notification;
        }

        notification = notification with
        {
            Recipient = notification.Recipient with
            {
                DeliveryInfo = deliveryInfo.Value
            }
        };

        return notification;
    }
}