namespace Api.Models.Requests.Common;

internal record TemplateContentRequest
{
    public required string? Subject { get; init; }
    public required string? Body { get; init; }
    public Dictionary<string, VariableDescriptorRequest>? Variables { get; init; }
}