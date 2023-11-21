using Api.Mappers.Common;
using Api.Models.Requests.Template;
using Api.Models.Responses;
using Core.Models.Common;
using Core.Models.Templates;

namespace Api.Mappers.Templates;

internal static class TemplateMapper
{
    public static TemplateDto ToDto(TemplateRequest r)
    {
        ContentDto? content = null;
        if (r.Content is not null)
        {
            content = ContentMapper.ToDto(r.Content);
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
        ContentResponse? content = null;
        if (dto.Content is not null)
        {
            content = ContentMapper.ToResponse(dto.Content);
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