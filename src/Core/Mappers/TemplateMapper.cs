﻿using Core.Models.Templates;
using Infrastructure.Persistence.Mongo.Entities;
using MongoDB.Bson;

namespace Core.Mappers;

internal static class TemplateMapper
{
    internal static TemplateDtoV2 ToDto(TemplateV2 e)
    {
        TemplateContentDto? content = null;
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

            content = new TemplateContentDto
            {
                Subject = e.Content.Subject,
                Body = e.Content.Body,
                Variables = variables
            };
        }

        return new TemplateDtoV2
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

    public static TemplateV2 ToEntity(TemplateDtoV2 dto)
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

        return new TemplateV2
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