using Api.Models.Requests.Auth;
using Api.Models.Responses;
using Core.Abstractions;
using FastEndpoints;
using Infrastructure.Auth0.Abstractions;
using Infrastructure.Auth0.Models;

namespace Api.Endpoints.Auth;

public class RegisterEndpoint(
    IAuthService authService,
    IUserPreferencesService userPreferencesService,
    ILogger<RegisterEndpoint> logger)
    : Endpoint<RegisterRequest, ApiResponse<UserInfoResponse>>
{
    private readonly IAuthService _authService = authService;
    private readonly IUserPreferencesService _userPreferencesService = userPreferencesService;
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
            var registrationResponse = await _authService.RegisterUser(
                new RegisterRequestDto(req.Login, req.FullName, req.Password, req.Email), ct);

            if (registrationResponse is null)
            {
                await SendUnauthorizedAsync(ct);
            }
            else
            {
                await _userPreferencesService.CreateUserPreferences(registrationResponse.UserId, ct);

                var tokenInfo = new UserTokenResponse(
                    registrationResponse.TokenInfo.Token,
                    registrationResponse.TokenInfo.ExpiresIn);

                var payload = new UserInfoResponse(registrationResponse.UserId, tokenInfo);
                await SendAsync(ApiResponse<UserInfoResponse>.Success(payload), cancellation: ct);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to login user {Login}", req.Login);
            throw;
        }
    }
}