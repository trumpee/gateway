using Core.Models.UserPreferences;
using ErrorOr;

namespace Core.Abstractions;

public interface IUserPreferencesService
{
    Task<ErrorOr<UserPreferencesDto>> CreateUserPreferences(string userId, CancellationToken ct);
    Task<ErrorOr<UserPreferencesDto>> UpdateUserPreferences(
        UserPreferencesDto userPreferences, CancellationToken ct);
}