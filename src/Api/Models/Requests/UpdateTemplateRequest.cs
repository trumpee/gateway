namespace Api.Endpoints.Templates;

internal record UpdateTemplateRequest
{
    public string Id { get; init; } = null!;
    public string? Name { get; init; }
    public string? TextTemplate { get; init; }
    public Dictionary<string, string>? DataChunksDescription { get; init; }
}