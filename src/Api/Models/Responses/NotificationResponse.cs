namespace Api.Models.Responses;

internal record NotificationResponse
{
    public required string Id { get; set; }
    public string? TemplateId { get; set; }
    public string? Status { get; set; }
    public DateTimeOffset? DeliveryTimestamp { get; set; }
}