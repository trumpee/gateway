using Api.Models.Requests.Template;
using Api.Models.Responses;
using Core.Models.Templates;

namespace Api.Mappers.Templates;

internal static class TemplateMapper
{
    public static TemplateDto ToDto(TemplateRequest r)
    {
        TemplateContentDto? content = null;
        if (r.Content is not null)
        {
            var variables = new Dictionary<string, VariableDescriptorDto>();
            if (r.Content.Variables is { Count: > 0 })
            {
                variables = r.Content.Variables.Values
                    .Select(x => new VariableDescriptorDto
                    {
                        Name = x.Name,
                        Description = x.Description,
                        Example = x.Example
                    }).ToDictionary(x => x.Name!, x => x);
            }

            content = new TemplateContentDto
            {
                Subject = r.Content.Subject,
                Body = r.Content.Body,
                Variables = variables
            };
        }

        return new TemplateDto
        {
            Id = r.Id,
            Name = r.Name,
            Description = r.Description,

            Content = content,

            ExcludedChannels = r.ExcludedChannels,
            CreationTimestamp = DateTimeOffset.UtcNow,
            LastModifiedTimestamp = DateTimeOffset.UtcNow
        };
    }

    public static TemplateResponse ToResponse(TemplateDto dto)
    {
        TemplateContentResponse? content = null;
        if (dto.Content is not null)
        {
            var variables = new Dictionary<string, VariableDescriptorResponse>();
            if (dto.Content.Variables is { Count: > 0 })
            {
                variables = dto.Content.Variables.Values
                    .Select(x => new VariableDescriptorResponse
                    {
                        Name = x.Name,
                        Description = x.Description,
                        Example = x.Example
                    }).ToDictionary(x => x.Name!, x => x);
            }

            content = new TemplateContentResponse
            {
                Subject = dto.Content.Subject,
                Body = dto.Content.Body,
                Variables = variables
            };
        }

        return new TemplateResponse
        {
            Id = dto.Id!,
            Name = dto.Name!,
            Description = dto.Description!,

            Content = content,

            ExcludedChannels = dto.ExcludedChannels,
            CreationTimestamp = DateTimeOffset.UtcNow,
            LastModifiedTimestamp = DateTimeOffset.UtcNow
        };
    }
}