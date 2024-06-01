namespace Api.Models.Responses;

public record UserTokenResponse(string Token, int ExpiresIn);