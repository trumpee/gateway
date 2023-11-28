using Core.Abstractions;
using Core.Mappers;
using Core.Models.Notifications;
using ErrorOr;
using Infrastructure.Persistence.MassTransit;
using Infrastructure.Persistence.Mongo.Abstractions;
using Trumpee.MassTransit.Messages.Notifications;

namespace Core.Services;

internal class NotificationService(
        IDeliveryRequestValidationClient massTransitClient,
        INotificationsRepository notificationsRepository)
    : INotificationsService
{
    public async Task<ErrorOr<NotificationDto>> CreateNotification(
        NotificationDto dto, CancellationToken ct)
    {
        var notification = NotificationMapper.ToEntity(dto);

        ct.ThrowIfCancellationRequested();
        await notificationsRepository.InsertOne(notification);

        var deliveryRequests = CreateDeliveryRequests(dto);

        await massTransitClient.SendMessages(deliveryRequests, string.Empty);

        dto = dto with { Id = notification.Id.ToString() };
        return dto;
    }

    private IEnumerable<Notification> CreateDeliveryRequests(NotificationDto dto)
    {
        return dto.Recipients!
            .Select(recipient => Mappers.External.DeliveryRequestMapper.ToRequest(dto, recipient))
            .ToList();
    }
}