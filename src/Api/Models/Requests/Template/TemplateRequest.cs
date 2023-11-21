using Api.Models.Requests.Common;
using Newtonsoft.Json;

namespace Api.Models.Requests.Template;

internal record TemplateRequest
{
    public string? Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }

    [JsonProperty("content")]
    public TemplateContentRequest? Content { get; init; }

    public string[]? ExcludedChannels { get; init; }
}