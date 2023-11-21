using Api.Models.Requests.Common;
using Newtonsoft.Json;

namespace Api.Models.Requests.Notification;

internal record CreateNotificationRequest
{
    public string? TemplateId { get; set; }
    public PriorityRequest PriorityRequest { get; set; }

    [JsonProperty("content")]
    public ContentRequest Content { get; set; }

    public DateTimeOffset DeliveryTimestamp { get; set; }
    public int RetryCount { get; set; }
    public string? Status { get; set; }
}