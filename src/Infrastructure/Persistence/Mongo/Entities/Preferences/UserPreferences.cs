namespace Infrastructure.Persistence.Mongo.Entities.Preferences;

public record UserPreferences : MongoBaseEntity
{
    public required string UserId { get; init; }
    public required Dictionary<string, ChannelDescriptorBase> Channels { get; init; }
    public DateTimeOffset LastUpdated { get; init; }
}