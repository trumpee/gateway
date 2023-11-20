namespace Core.Models.Templates;

public record TemplateDto
{
    public string? Id { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }

    public TemplateContentDto? Content { get; set; }

    public string[]? ExcludedChannels { get; init; }

    public DateTimeOffset? CreationTimestamp { get; init; }
    public DateTimeOffset? LastModifiedTimestamp { get; init; }
}

public class TemplateContentDto
{
    public string? Subject { get; init; }
    public string? Body { get; init; }

    public Dictionary<string, VariableDescriptorDto>? Variables { get; set; }
}

public class VariableDescriptorDto
{
    public string? Name { get; init; }
    public string? Example { get; init; }
    public string? Description { get; init; }
}
