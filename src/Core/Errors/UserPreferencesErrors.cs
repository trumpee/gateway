using Core.Services;
using ErrorOr;

namespace Core.Errors;

public class UserPreferencesErrors
{
    internal static ErrorOr<UserPreferencesDto> FailedToCreate
        => Error.Unexpected("UserPreferences.CreateFailed", "Failed to create user preferences");
}