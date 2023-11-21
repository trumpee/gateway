
using Core.Models.Common;

namespace Core.Models.Notifications;

public record NotificationDto
{
    public string? Id { get; set; }

    public string? TemplateId { get; set; }
    public Priority Priority { get; set; }

    public ContentDto? Content { get; set; }

    public DateTimeOffset DeliveryTimestamp { get; set; }
    public int RetryCount { get; set; }
    public string? Status { get; set; }
}