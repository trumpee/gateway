using Core.Models.Common;
using Core.Models.Templates;
using Infrastructure.Persistence.Mongo.Entities.Common;
using Infrastructure.Persistence.Mongo.Entities.Template;
using MongoDB.Bson;

namespace Core.Mappers;

internal static class TemplateMapper
{
    internal static TemplateDto ToDto(Template e)
    {
        ContentDto? content = null;
        if (e.Content is not null)
        {
            content = ContentMapper.ToDto(e.Content);
        }

        return new TemplateDto
        {
            Id = e.Id.ToString(),
            Name = e.Name!,
            Description = e.Description!,

            Content = content,

            ExcludedChannels = e.ExcludedChannels,
            CreationTimestamp = DateTimeOffset.UtcNow,
            LastModifiedTimestamp = DateTimeOffset.UtcNow
        };

    }

    public static Template ToEntity(TemplateDto dto)
    {
        var id = string.IsNullOrEmpty(dto.Id)
            ? ObjectId.GenerateNewId()
            : ObjectId.Parse(dto.Id);

        Content? content = null;
        if (dto.Content is not null)
        {
            content = ContentMapper.ToEntity(dto.Content);
        }

        return new Template
        {
            Id = id,
            Name = dto.Name!,
            Description = dto.Description!,

            Content = content,

            ExcludedChannels = dto.ExcludedChannels,
            CreationTimestamp = DateTimeOffset.UtcNow,
            LastModifiedTimestamp = DateTimeOffset.UtcNow
        };
    }
}