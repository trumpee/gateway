namespace Infrastructure.Persistence.Mongo.Entities.Preferences;

public record UserPreferences : MongoBaseEntity
{
    public required string UserId { get; set; }
    public Dictionary<string, ChannelDescriptorBase>? Channels { get; set; }
    public DateTimeOffset LastUpdated { get; set; }
}