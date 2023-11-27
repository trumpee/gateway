using Infrastructure.Persistence.Mongo.Entities.Common;

namespace Infrastructure.Persistence.Mongo.Entities.Notifications;

public record Notification : MongoBaseEntity
{
    public string? TemplateId { get; init; }
    public Priority Priority { get; init; }

    public Content? Content { get; init; }
    public Recipient[]? Recipients { get; set; }

    public DateTimeOffset? DeliveryTimestamp { get; init; }
    public int RetryCount { get; init; }
    public string? Status { get; init; }
}