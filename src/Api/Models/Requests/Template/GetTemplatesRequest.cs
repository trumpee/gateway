namespace Api.Models.Requests.Template;

public record GetTemplatesRequest
{
    public string[]? Ids { get; init; }
    public string[]? Names { get; init; }
    public bool? All { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
}