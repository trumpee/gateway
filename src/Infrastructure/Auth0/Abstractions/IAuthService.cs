using Infrastructure.Auth0.Models;

namespace Infrastructure.Auth0.Abstractions;

public interface IAuthService
{
    Task<TokenInfoDto?> GetUserToken(LoginRequestDto req, CancellationToken ct);
    Task<TokenInfoDto?> RegisterUser(RegisterRequestDto req, CancellationToken ct);
}