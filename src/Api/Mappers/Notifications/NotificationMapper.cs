using Api.Mappers.Common;
using Api.Models.Requests.Notification;
using Api.Models.Responses;
using Core.Models.Common;
using Core.Models.Notifications;

namespace Api.Mappers.Notifications;

internal static class NotificationMapper
{
    public static NotificationDto ToDto(NotificationRequest e)
    {
        ContentDto? contentDto = null;
        if (e.Content is { } c)
        {
            contentDto = ContentMapper.ToDto(c);
        }

        return new NotificationDto
        {
            TemplateId = e.TemplateId,
            Priority = (Priority)e.Priority,
            Content = contentDto,
            Recipients = e.Recipients
                .Select(RecipientMapper.ToDto)
                .ToArray(),
            RetryCount = e.RetryCount,
            DeliveryTimestamp = e.DeliveryTimestamp
        };
    }

    public static NotificationResponse ToResponse(NotificationDto e)
    {
        return new NotificationResponse
        {
            Id = e.Id!,
            TemplateId = e.TemplateId,
            Status = e.Status,
            DeliveryTimestamp = e.DeliveryTimestamp
        };
    }
}