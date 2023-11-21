using Infrastructure.Persistence.Mongo.Entities.Common;
using MongoDB.Bson;

namespace Infrastructure.Persistence.Mongo.Entities.Notifications;

public record Notification
{
    public ObjectId Id { get; init; }

    public string? TemplateId { get; init; }
    public Priority Priority { get; init; }

    public Content? Content { get; init; }

    public DateTimeOffset DeliveryTimestamp { get; init; }
    public int RetryCount { get; init; }
    public string? Status { get; init; }
}