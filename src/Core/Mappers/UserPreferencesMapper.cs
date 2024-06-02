using Core.Services;
using Infrastructure.Persistence.Mongo.Entities.Preferences;
using MongoDB.Bson;

namespace Core.Mappers;

public class UserPreferencesMapper
{
    internal static UserPreferencesDto ToDto(UserPreferences e)
    {
        return new UserPreferencesDto
        {
            Id = e.Id.ToString(),
            UserId = e.UserId,
            Channels = e.Channels?.ToDictionary(x => x.Key, d => new ChannelDescriptorBaseDto
            {
                Enabled = d.Value.Enabled,
                Description = d.Value.Description,
                Metadata = d.Value.Metadata
            }) ?? []
        };
    }
    
    public static UserPreferences ToEntity(UserPreferencesDto dto)
    {
        var id = string.IsNullOrEmpty(dto.Id)
            ? ObjectId.GenerateNewId()
            : ObjectId.Parse(dto.Id);

        return new UserPreferences
        {
            Id = id,
            UserId = dto.UserId,
            Channels = dto.Channels.ToDictionary(x => x.Key, d => new ChannelDescriptorBase
            {
                Enabled = d.Value.Enabled,
                Description = d.Value.Description,
                Metadata = d.Value.Metadata
            })
        };
    }
}