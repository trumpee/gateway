namespace Infrastructure.Persistence.Mongo.Entities.Notifications;

public record Recipient
{
    public string? UserId { get; init; }
    public string? Channel { get; init; }
}