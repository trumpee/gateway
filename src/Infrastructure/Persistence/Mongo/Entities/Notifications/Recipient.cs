namespace Infrastructure.Persistence.Mongo.Entities.Notifications;

internal class Recipient
{
    public string? UserId { get; set; }
    public string? Channel { get; set; }
}