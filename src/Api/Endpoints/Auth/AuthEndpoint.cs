using Api.Models.Requests.Auth;
using Api.Models.Responses;
using FastEndpoints;
using Infrastructure.Auth0.Abstractions;
using Infrastructure.Auth0.Models;

namespace Api.Endpoints.Auth;

public class LoginEndpoint(
    IAuthService authService,
    ILogger<RegisterEndpoint> logger)
    : Endpoint<LoginRequest, ApiResponse<UserTokenResponse>>
{
    private readonly IAuthService _authService = authService;
    private readonly ILogger<RegisterEndpoint> _logger = logger;

    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("api/auth/login");
    }
    
    public override async Task HandleAsync(
        LoginRequest req,
        CancellationToken ct)
    {
        try
        {
            var tokenInfo = await _authService.GetUserToken(
                new LoginRequestDto(req.Login, req.Password), ct);

            if (tokenInfo is null)
            {
                await SendUnauthorizedAsync(ct);
            }
            else
            {
                var payload = new UserTokenResponse(tokenInfo.Token, tokenInfo.ExpiresIn);
                await SendAsync(ApiResponse<UserTokenResponse>.Success(payload), cancellation: ct);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to login user {Login}", req.Login);
            throw;
        }
    }
}