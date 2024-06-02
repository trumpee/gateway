using Core.Models.UserPreferences;
using Core.Services;
using ErrorOr;

namespace Core.Errors;

public static class UserPreferencesErrors
{
    internal static ErrorOr<UserPreferencesDto> FailedToCreate
        => Error.Unexpected("UserPreferences.CreateFailed", "Failed to create user preferences");

    internal static ErrorOr<UserPreferencesDto> FailedToUpdate
        => Error.Unexpected("UserPreferences.CreateUpdate", "Failed to update user preferences");

    internal static ErrorOr<UserPreferencesDto> NotFound
        => Error.NotFound("UserPreferences.NotFound", "User preferences not found");
}