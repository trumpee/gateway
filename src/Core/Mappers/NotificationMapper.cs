using Core.Models.Common;
using Core.Models.Notifications;
using Infrastructure.Persistence.Mongo.Entities.Common;
using Infrastructure.Persistence.Mongo.Entities.Notifications;
using MongoDB.Bson;
using PriorityDto = Core.Models.Notifications.Priority;
using PriorityEntity = Infrastructure.Persistence.Mongo.Entities.Notifications.Priority;

namespace Core.Mappers;

internal static class NotificationMapper
{
    public static NotificationDto ToDto(Notification e)
    {
        ContentDto? contentDto = null;
        if (e.Content is { } c)
        {
            contentDto = ContentMapper.ToDto(c);
        }

        return new NotificationDto
        {
            Id = e.Id.ToString(),
            TemplateId = e.TemplateId,
            Priority = (PriorityDto)e.Priority,
            Content = contentDto,
            Status = e.Status,
            RetryCount = e.RetryCount,
            DeliveryTimestamp = e.DeliveryTimestamp
        };
    }

    public static Notification ToEntity(NotificationDto e)
    {
        var id = string.IsNullOrEmpty(e.Id)
            ? ObjectId.GenerateNewId()
            : ObjectId.Parse(e.Id);

        Content? contentDto = null;
        if (e.Content is { } c)
        {
            contentDto = ContentMapper.ToEntity(c);
        }

        return new Notification
        {
            Id = id,
            TemplateId = e.TemplateId,
            Priority = (PriorityEntity)e.Priority,
            Content = contentDto,
            Status = e.Status,
            RetryCount = e.RetryCount,
            DeliveryTimestamp = e.DeliveryTimestamp
        };
    }
}