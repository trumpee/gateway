using ErrorOr;

namespace Core.Services;

public interface IUserPreferencesService
{
    Task<ErrorOr<UserPreferencesDto>> CreateUserPreferences(string userId);
    Task<ErrorOr<UserPreferencesDto>> UpdateUserPreferences(string userId, UserPreferencesDto userPreferences);
}