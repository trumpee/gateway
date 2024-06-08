namespace Infrastructure.Auth0.Configuration;

public record Auth0Options
{
    public const string SectionName = "Auth0";
    
    public required string ClientId { get; init; }
    public required string ClientSecret { get; init; }
    public required string Domain { get; init; }
    public required string Authority { get; init; }
    public required string Audience { get; init; }
    public required string Realm { get; init; }
}