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