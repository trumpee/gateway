using Api.Extensions;
using Api.Models.Requests.Notification;
using Api.Models.Responses;
using Core.Models.Notifications;
using ErrorOr;
using FastEndpoints;

namespace Api.Mappers.Notifications;

internal class CreateNotificationMapper :
    Mapper<CreateNotificationRequest, ApiResponse<NotificationResponse>, ErrorOr<NotificationDto>>
{
    public override ErrorOr<NotificationDto> ToEntity(CreateNotificationRequest r)
    {
        return NotificationMapper.ToDto(r);
    }

    public override ApiResponse<NotificationResponse> FromEntity(ErrorOr<NotificationDto> e)
    {
        return e.ToApiResponse(NotificationMapper.ToResponse);
    }
}