namespace Api.Models.Requests.UserPreferences;

public class UpdateUserPreferencesRequest
{
    public required string UserId { get; set; }
    public required Dictionary<string, ChannelDescriptorBaseRequest> Channels { get; set; }
}