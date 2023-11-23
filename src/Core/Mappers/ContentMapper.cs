using Core.Models.Common;
using Infrastructure.Persistence.Mongo.Entities.Common;

namespace Core.Mappers;

internal static class ContentMapper
{
    internal static ContentDto ToDto(Content e)
    {
        var variables = new Dictionary<string, VariableDescriptorDto>();
        if (e.Variables is { Count: > 0 })
        {
            variables = e.Variables.Values
                .Select(VariableDescriptorMapper.ToDto)
                .ToDictionary(x => x.Name!, x => x);
        }

        var content = new ContentDto
        {
            Subject = e.Subject,
            Body = e.Body,
            Variables = variables
        };

        return content;
    }

    internal static Content ToEntity(ContentDto e)
    {
        var variables = new Dictionary<string, VariableDescriptor>();
        if (e.Variables is { Count: > 0 })
        {
            variables = e.Variables.Values
                .Select(VariableDescriptorMapper.ToEntity)
                .ToDictionary(x => x.Name!, x => x);
        }

        var content = new Content
        {
            Subject = e.Subject,
            Body = e.Body,
            Variables = variables
        };

        return content;
    }
}