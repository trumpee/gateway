using Newtonsoft.Json;

namespace Api.Models.Requests;

internal record TemplateRequest
{
    public string? Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }

    [JsonProperty("content")]
    public TemplateContentRequest? Content { get; init; }

    public string[]? ExcludedChannels { get; init; }
}

internal record TemplateContentRequest
{
    public required string? Subject { get; init; }
    public required string? Body { get; init; }
    public Dictionary<string, VariableDescriptorRequest>? Variables { get; init; }
}

internal class VariableDescriptorRequest
{
    public required string? Name { get; init; }
    public string? Example { get; init; }
    public string? Description { get; init; }
}