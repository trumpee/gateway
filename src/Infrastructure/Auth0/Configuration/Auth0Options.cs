namespace Infrastructure.Auth0.Configuration;

public record Auth0Options(
        string ClientId,
        string ClientSecret,
        string Domain,
        string Authority,
        string Audience,
        string Realm
    )
{
    public const string SectionName = "Auth0";
}