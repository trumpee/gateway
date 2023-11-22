using Core.Models.Notifications;
using ErrorOr;

namespace Core.Abstractions;

public interface INotificationsService
{
    Task<ErrorOr<NotificationDto>> CreateNotification(NotificationDto dto, CancellationToken ct);
}