namespace Core.Services;

public record UserPreferencesDto
{
    public string? Id { get; init; }
    public required string UserId { get; init; }
    public required Dictionary<string, ChannelDescriptorBaseDto> Channels { get; init; }
}