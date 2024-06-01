namespace Infrastructure.Auth0.Models;

public record TokenInfoDto(string Token, int ExpiresIn);