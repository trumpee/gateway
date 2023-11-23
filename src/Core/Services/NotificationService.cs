using Core.Abstractions;
using Core.Mappers;
using Core.Models.Notifications;
using ErrorOr;
using Infrastructure.Persistence.Mongo.Abstractions;

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

        dto = dto with { Id = notification.Id.ToString() };
        return dto;
    }
}