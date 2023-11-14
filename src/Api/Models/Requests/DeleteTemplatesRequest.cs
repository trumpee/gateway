namespace Api.Models.Requests;

public record DeleteTemplatesRequest
{
    public string[]? Ids { get; set; }
    public string[]? Names { get; set; }
}