using Core.Abstractions;
using Core.Errors;
using Core.Mappers;
using Core.Models.UserPreferences;
using ErrorOr;
using Infrastructure.Persistence.Mongo.Abstractions;
using Infrastructure.Persistence.Mongo.Entities.Preferences;
using Infrastructure.Persistence.Mongo.Specifications;
using Microsoft.Extensions.Logging;

namespace Core.Services;

public class UserPreferencesService(
    IMongoRepository<UserPreferences> userPreferencesRepository,
    ILogger<UserPreferencesService> logger) : IUserPreferencesService
{
    private readonly IMongoRepository<UserPreferences> _userPreferencesRepository = userPreferencesRepository;
    private readonly ILogger<UserPreferencesService> _logger = logger;
    
    public async Task<ErrorOr<UserPreferencesDto>> CreateUserPreferences(string userId, CancellationToken ct)
    {
        try
        {
            var defaultPreferences = new UserPreferencesDto
            {
                UserId = userId,
                Channels = new Dictionary<string, ChannelDescriptorBaseDto>()
            };
            
            var entity = UserPreferencesMapper.ToEntity(defaultPreferences);
            await _userPreferencesRepository.InsertOne(entity);
            
            return UserPreferencesMapper.ToDto(entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create user preferences");
            return UserPreferencesErrors.FailedToCreate;
        }
    }
    
    public async Task<ErrorOr<UserPreferencesDto>> UpdateUserPreferences(
        UserPreferencesDto userPreferences, CancellationToken ct)
    {
        try
        {
            var byUserIdSpec = new AdHocSpecification<UserPreferences>(x => x.UserId == userPreferences.UserId);
            var currentPreferences = await _userPreferencesRepository.FirstOrDefault(byUserIdSpec);
            if (currentPreferences is null)
            {
                var result = await CreateUserPreferences(userPreferences.UserId, ct);
                return result;
            }

            var entity = UserPreferencesMapper.UpdateEntity(currentPreferences, userPreferences);
            await _userPreferencesRepository.Replace(entity);
            
            return UserPreferencesMapper.ToDto(entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update user preferences");
            return UserPreferencesErrors.FailedToUpdate;
        }
    }
}