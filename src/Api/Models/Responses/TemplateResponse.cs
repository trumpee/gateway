namespace Api.Models.Responses;

internal record TemplateResponse
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string TextTemplate { get; set; }
    public Dictionary<string, string>? DataChunksDescription { get; set; }
}