using Core.Models.Common;

namespace Core.Models.Templates;

public record TemplateDto
{
    public string? Id { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }

    public ContentDto? Content { get; set; }

    public string[]? ExcludedChannels { get; init; }

    public DateTimeOffset? CreationTimestamp { get; init; }
    public DateTimeOffset? LastModifiedTimestamp { get; init; }
}