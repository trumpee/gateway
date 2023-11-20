namespace Api.Models.Responses;

internal record TemplateResponse
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }

    public TemplateContentResponse? Content { get; set; }

    public string[]? ExcludedChannels { get; set; }
    public DateTimeOffset CreationTimestamp { get; set; }
    public DateTimeOffset LastModifiedTimestamp { get; set; }
}

internal record TemplateContentResponse
{
    public string? Subject { get; init; }
    public string? Body { get; init; }

    public Dictionary<string, VariableDescriptorResponse>? Variables { get; set; }
}

public class VariableDescriptorResponse
{
    public string? Name { get; init; }
    public string? Example { get; init; }
    public string? Description { get; init; }
}