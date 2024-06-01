using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using Infrastructure.Auth0.Abstractions;
using Infrastructure.Auth0.Configuration;
using Infrastructure.Auth0.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.Auth0;

public class AuthService(
    IOptions<Auth0Options> auth0Options,
    IAuthenticationApiClient authenticationClient,
    ILogger<AuthService> logger) : IAuthService
{
    private readonly Auth0Options _auth0Options = auth0Options.Value ??
                                                  throw new ArgumentNullException(nameof(auth0Options));
    private readonly IAuthenticationApiClient _auth0Client = authenticationClient;
    private readonly ILogger<AuthService> _logger = logger;
    
    public async Task<TokenInfoDto?> GetUserToken(LoginRequestDto req, CancellationToken ct)
    {
        try
        {
            var auth0Response = await _auth0Client.GetTokenAsync(new ResourceOwnerTokenRequest
            {
                Username = req.Login,
                Password = req.Password,

                Realm = _auth0Options.Realm,
                Audience = _auth0Options.Audience,
                ClientId = _auth0Options.ClientId,
                ClientSecret = _auth0Options.ClientSecret,
                Scope = "openid"
            }, ct);
            
            var authToken = auth0Response?.AccessToken;
            if (string.IsNullOrEmpty(authToken))
            {
                _logger.LogWarning("Failed to get token for user {Login}", req.Login);
                return null;
            }
            
            return new TokenInfoDto(authToken, auth0Response?.ExpiresIn ?? 0);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get token for user {Login}", req.Login);
            return null;
        }
    }
    
    public async Task<TokenInfoDto?> RegisterUser(RegisterRequestDto req, CancellationToken ct)
    {
        try
        {
            var username = $"{req.FullName[0]}-{Guid.NewGuid().ToString()[..6]}";
            await _auth0Client.SignupUserAsync(new SignupUserRequest
            {
                Username = username,
                Nickname = username,
                Name = req.FullName ,
                Password = req.Password,
                Email = req.Email,

                UserMetadata = new Dictionary<string, object>
                {
                    ["trumpee_uid"] = username
                },

                Connection =_auth0Options.Realm,
                ClientId = _auth0Options.ClientId
            }, ct);
            
            return await GetUserToken(new LoginRequestDto(username, req.Password), ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to register user {Login}", req.Login);
            return null;
        }
    }
}