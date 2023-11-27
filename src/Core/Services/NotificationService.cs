using Core.Abstractions;
using Core.Mappers;
using Core.Models.Notifications;
using ErrorOr;
using Infrastructure.Persistence.Mongo.Abstractions;
using Trumpee.MassTransit.Messages.Notifications;

namespace Core.Services;

internal class NotificationService : INotificationsService
{
    private readonly INotificationsRepository _notificationsRepository;

    public NotificationService(INotificationsRepository notificationsRepository)
    {
        _notificationsRepository = notificationsRepository;
    }

    public async Task<ErrorOr<NotificationDto>> CreateNotification(
        NotificationDto dto, CancellationToken ct)
    {
        var notification = NotificationMapper.ToEntity(dto);

        ct.ThrowIfCancellationRequested();
        await _notificationsRepository.InsertOne(notification);

        var deliveryRequests = CreateDeliveryRequests(dto);
        // TODO: push delivery requests to queue

        dto = dto with { Id = notification.Id.ToString() };
        return dto;
    }

    private Task<ErrorOr<List<Notification>>> CreateDeliveryRequests(
        NotificationDto dto)
    {
        var requests = dto.Recipients!
            .Select(recipient => Mappers.External.DeliveryRequestMapper.ToRequest(dto, recipient))
            .ToList();

        return Task.FromResult<ErrorOr<List<Notification>>>(requests);
    }
}