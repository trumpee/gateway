using Infrastructure.Persistence.Mongo.Entities.Common;

namespace Infrastructure.Persistence.Mongo.Entities.Notifications;

internal record Notification
{
    public string? Id { get; set; }

    public string? TemplateId { get; set; }
    public Priority Priority { get; set; }

    public Content? Content { get; set; }

    public DateTimeOffset DeliveryTimestamp { get; set; }
    public int RetryCount { get; set; }
    public string? Status { get; set; }
}