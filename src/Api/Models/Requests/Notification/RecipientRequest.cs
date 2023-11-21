namespace Api.Models.Requests.Notification;

internal class RecipientRequest
{
    public string? UserId { get; set; }
    public string? Channel { get; set; }
}