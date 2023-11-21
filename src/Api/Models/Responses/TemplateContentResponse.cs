namespace Api.Models.Responses;

internal record TemplateContentResponse
{
    public string? Subject { get; init; }
    public string? Body { get; init; }

    public Dictionary<string, VariableDescriptorResponse>? Variables { get; set; }
}