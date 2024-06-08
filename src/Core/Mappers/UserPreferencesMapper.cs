using Core.Extensions;
using Core.Models.UserPreferences;
using Infrastructure.Persistence.Mongo.Entities.Preferences;
using MongoDB.Bson;

namespace Core.Mappers;

public static class UserPreferencesChannelMapper
{
    public static ChannelDescriptorBaseDto ToDto(ChannelDescriptorBase e)
    {
        return new ChannelDescriptorBaseDto
        {
            Enabled = e.Enabled,
            Description = e.Description,
            Metadata = e.Metadata
        };
    }
    
    public static ChannelDescriptorBase ToEntity(ChannelDescriptorBaseDto dto)
    {
        return new ChannelDescriptorBase
        {
            Enabled = dto.Enabled,
            Description = dto.Description,
            Metadata = dto.Metadata
        };
    }
}

public static class UserPreferencesMapper
{
    internal static UserPreferencesDto ToDto(UserPreferences e)
    {
        return new UserPreferencesDto
        {
            Id = e.Id.ToString(),
            UserId = e.UserId,
            Channels = e.Channels.ToDictionary(x => x.Key, d => UserPreferencesChannelMapper.ToDto(d.Value))
        };
    }
    
    public static UserPreferences UpdateEntity(UserPreferences e, UserPreferencesDto dto)
    {
        var channels = e.Channels;
        foreach (var ch in dto.Channels)
        {
            var metadataKeys = ch.Value.Metadata?.Keys.ToArray()!;
            foreach (var mk in metadataKeys)
            {
                var normalized = mk.ToPascalCase();

                if (!mk.Equals(normalized))
                {
                    ch.Value.Metadata![normalized] = ch.Value.Metadata[mk];
                    ch.Value.Metadata.Remove(mk);
                }
            }

            var normalizedKey = ch.Key.ToPascalCase();
            if (channels.TryGetValue(normalizedKey, out var channel))
            {
                channel.Enabled = ch.Value.Enabled;
                channel.Description = ch.Value.Description;
                channel.Metadata = ch.Value.Metadata;
            }
            else
            {
                channels.Add(normalizedKey, new ChannelDescriptorBase
                {
                    Enabled = ch.Value.Enabled,
                    Description = ch.Value.Description,
                    Metadata = ch.Value.Metadata
                });
            }
        }
        
        return e with { LastUpdated = DateTimeOffset.UtcNow, Channels = channels };
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
            Channels = dto.Channels.ToDictionary(x => x.Key, d => UserPreferencesChannelMapper.ToEntity(d.Value)),
            LastUpdated = DateTimeOffset.UtcNow
        };
    }
}