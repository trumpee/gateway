namespace Api.Models.Responses;

public record UserInfoResponse(string UserId, UserTokenResponse TokenInfo);