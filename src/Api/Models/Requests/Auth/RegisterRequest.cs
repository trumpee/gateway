namespace Api.Models.Requests.Auth;

public record RegisterRequest(string Login, string FullName, string Password, string Email);
