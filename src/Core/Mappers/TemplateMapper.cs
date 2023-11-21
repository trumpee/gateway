using Core.Models.Common;
using Core.Models.Templates;
using Infrastructure.Persistence.Mongo.Entities;
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
            var variables = new Dictionary<string, VariableDescriptorDto>();
            if (e.Content.Variables is { Count: > 0 })
            {
                variables = e.Content.Variables.Values
                    .Select(x => new VariableDescriptorDto
                    {
                        Name = x.Name,
                        Description = x.Description,
                        Example = x.Example
                    }).ToDictionary(x => x.Name!, x => x);
            }

            content = new ContentDto
            {
                Subject = e.Content.Subject,
                Body = e.Content.Body,
                Variables = variables
            };
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

        TemplateContent? content = null;
        if (dto.Content is not null)
        {
            var variables = new Dictionary<string, VariableDescriptor>();
            if (dto.Content.Variables is { Count: > 0 })
            {
                variables = dto.Content.Variables.Values
                    .Select(x => new VariableDescriptor
                    {
                        Name = x.Name,
                        Description = x.Description,
                        Example = x.Example
                    }).ToDictionary(x => x.Name!, x => x);
            }

            content = new TemplateContent
            {
                Subject = dto.Content.Subject,
                Body = dto.Content.Body,
                Variables = variables
            };
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