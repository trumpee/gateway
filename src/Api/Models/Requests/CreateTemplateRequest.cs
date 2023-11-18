namespace Api.Models.Requests;

public record CreateTemplateRequest
{
    public required string Name { get; set; }
    public required string TextTemplate { get; set; }
    public Dictionary<string, string>? DataChunksDescription { get; set; }
}