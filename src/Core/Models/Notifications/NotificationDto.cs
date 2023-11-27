using Core.Models.Common;

namespace Core.Models.Notifications;

public record NotificationDto
{
    public string? Id { get; init; }

    public string? TemplateId { get; init; }
    public Priority Priority { get; init; }

    public ContentDto? Content { get; init; }
    public RecipientDto[]? Recipients { get; init; }

    public DateTimeOffset? DeliveryTimestamp { get; init; }
    public int RetryCount { get; init; }
    public string? Status { get; init; }
}