namespace Api.Models.Requests;

public record DeleteTemplatesRequest
{
    public string[]? Ids { get; init; }
    public string[]? Names { get; init; }
}