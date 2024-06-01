using Api.Models.Requests.Auth;
using Api.Models.Responses;
using FastEndpoints;
using Infrastructure.Auth0.Abstractions;
using Infrastructure.Auth0.Models;

namespace Api.Endpoints.Auth;

public class RegisterEndpoint(
    IAuthService authService,
    ILogger<RegisterEndpoint> logger)
    : Endpoint<RegisterRequest, ApiResponse<UserTokenResponse>>
{
    private readonly IAuthService _authService = authService;
    private readonly ILogger<RegisterEndpoint> _logger = logger;

    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("api/auth/register");
    }

    public override async Task HandleAsync(
        RegisterRequest req,
        CancellationToken ct)
    {
        try
        {
            var tokenInfo = await _authService.RegisterUser(
                new RegisterRequestDto(req.Login, req.FullName, req.Password, req.Email), ct);

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