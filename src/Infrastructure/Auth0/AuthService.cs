using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using Infrastructure.Auth0.Abstractions;
using Infrastructure.Auth0.Configuration;
using Infrastructure.Auth0.Models;
using Infrastructure.Persistence.MassTransit.Analytics;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.Auth0;

public class AuthService(
    IOptions<Auth0Options> auth0Options,
    IAuthenticationApiClient authenticationClient,
    IUserAnalyticsClient userAnalyticsClient,
    ILogger<AuthService> logger) : IAuthService
{
    private readonly Auth0Options _auth0Options = auth0Options.Value ??
                                                  throw new ArgumentNullException(nameof(auth0Options));
    private readonly IAuthenticationApiClient _auth0Client = authenticationClient;
    private readonly IUserAnalyticsClient _userAnalyticsClient = userAnalyticsClient;
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

            await _userAnalyticsClient.SendUserSignIn(req.Login, ct);
            return new TokenInfoDto(authToken, auth0Response?.ExpiresIn ?? 0);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get token for user {Login}", req.Login);
            return null;
        }
    }
    
    public async Task<UserInfoDto?> RegisterUser(RegisterRequestDto req, CancellationToken ct)
    {
        try
        {
            var userId = await CreateAuth0Account(req, ct);
            await _userAnalyticsClient.SendUserSignUp(userId, req.Email, req.FullName, "recipient", ct);

            var tokenInfo = await SignInUserInternal(req, ct);
            return new UserInfoDto(userId, tokenInfo!);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to register user {Login}", req.Login);
            return null;
        }
    }
    
    private async Task<string> CreateAuth0Account(RegisterRequestDto req, CancellationToken ct)
    {
        var username = $"{req.FullName[0]}-{Guid.NewGuid().ToString()[..6]}".ToLower();
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

        return username;
    }
    
    private async Task<TokenInfoDto?> SignInUserInternal(RegisterRequestDto req, CancellationToken ct)
    {
        var tokenInfo = await GetUserToken(new LoginRequestDto(req.Email, req.Password), ct);
        if (tokenInfo is null)
        {
            _logger.LogWarning("Failed to login recently registered user {Login}", req.Login);
            return tokenInfo;
        }
        
        return tokenInfo;
    }
}