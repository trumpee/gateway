namespace Core.Models.Templates;

public record TemplateDto
{
    public string? Id { get; init; }
    public required string Name { get; init; }
    public required string TextTemplate { get; init; }
    public Dictionary<string, string>? DataChunksDescription { get; init; }
}