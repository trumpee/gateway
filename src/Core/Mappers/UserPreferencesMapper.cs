using Core.Extensions;
using Core.Models.UserPreferences;
using Infrastructure.Persistence.Mongo.Entities.Preferences;
using MongoDB.Bson;

namespace Core.Mappers;

public static class UserPreferencesMapper
{
    internal static UserPreferencesDto ToDto(UserPreferences e)
    {
        return new UserPreferencesDto
        {
            Id = e.Id.ToString(),
            UserId = e.UserId,
            Channels = e.Channels.ToDictionary(x => x.Key, d => new ChannelDescriptorBaseDto
            {
                Enabled = d.Value.Enabled,
                Description = d.Value.Description,
                Metadata = d.Value.Metadata
            })
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
            Channels = dto.Channels.ToDictionary(x => x.Key, d => new ChannelDescriptorBase
            {
                Enabled = d.Value.Enabled,
                Description = d.Value.Description,
                Metadata = d.Value.Metadata
            }),
            LastUpdated = DateTimeOffset.UtcNow
        };
    }
}