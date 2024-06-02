namespace Api.Models.Requests.UserPreferences;

public record ChannelDescriptorBaseRequest
{
    public bool Enabled { get; init; }
    public string? Description { get; init; }
    public Dictionary<string, string>? Metadata { get; init; }
}