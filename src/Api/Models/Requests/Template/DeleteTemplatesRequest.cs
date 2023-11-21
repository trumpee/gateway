namespace Api.Models.Requests.Template;

public record DeleteTemplatesRequest
{
    public string[]? Ids { get; init; }
    public string[]? Names { get; init; }
}