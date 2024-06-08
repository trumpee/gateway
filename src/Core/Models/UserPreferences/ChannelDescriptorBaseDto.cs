namespace Core.Models.UserPreferences;

public record ChannelDescriptorBaseDto
{
    public bool Enabled { get; init; }
    public string? Description { get; init; }
    public Dictionary<string, string>? Metadata { get; init; }
}