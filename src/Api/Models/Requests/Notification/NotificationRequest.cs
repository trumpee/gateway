using Api.Models.Requests.Common;
using Newtonsoft.Json;

namespace Api.Models.Requests.Notification;

internal record NotificationRequest
{
    public string? TemplateId { get; set; }
    public PriorityRequest Priority { get; set; }

    [JsonProperty("content")]
    public ContentRequest? Content { get; set; }
    public required RecipientRequest[] Recipients { get; set; }
    public DateTimeOffset? DeliveryTimestamp { get; set; }
    public int RetryCount { get; set; }
}