namespace Core.Models.Templates;

public record TemplatesFilterDto
{
    public bool? All { get; set; }
    public string[]? Ids { get; init; }
    public string[]? Names { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
}
