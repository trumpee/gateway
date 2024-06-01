namespace Infrastructure.Auth0.Models;

public record RegisterRequestDto(string Login, string FullName, string Password, string Email);
