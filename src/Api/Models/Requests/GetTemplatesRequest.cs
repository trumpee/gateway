namespace Api.Models.Requests;

public record GetTemplatesRequest
{
    public string[]? Ids { get; set; }
    public string[]? Names { get; set; }
    public bool? All { get; set; }
}