using Api.Models.Requests.Common;
using Api.Models.Responses;
using Core.Models.Common;

namespace Api.Mappers.Common;

internal static class ContentMapper
{
    internal static ContentDto ToDto(ContentRequest e)
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

    internal static ContentResponse ToResponse(ContentDto e)
    {
        var variables = new Dictionary<string, VariableDescriptorResponse>();
        if (e.Variables is { Count: > 0 })
        {
            variables = e.Variables.Values
                .Select(VariableDescriptorMapper.ToResponse)
                .ToDictionary(x => x.Name!, x => x);
        }

        var content = new ContentResponse
        {
            Subject = e.Subject,
            Body = e.Body,
            Variables = variables
        };

        return content;
    }
}