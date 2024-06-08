namespace Infrastructure.Persistence.Mongo.Entities.Preferences;

public record ChannelDescriptorBase
{
    public bool Enabled { get; set; }
    public string? Description { get; set; }
    public Dictionary<string, string>? Metadata { get; set; }
}