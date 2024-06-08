using Core.Abstractions;
using Core.Errors;
using Core.Extensions;
using Core.Mappers;
using Core.Models.UserPreferences;
using ErrorOr;
using Infrastructure.Persistence.MassTransit.Analytics;
using Infrastructure.Persistence.Mongo.Abstractions;
using Infrastructure.Persistence.Mongo.Entities.Preferences;
using Infrastructure.Persistence.Mongo.Specifications;
using Microsoft.Extensions.Logging;

namespace Core.Services;

public class UserPreferencesService(
    IUserAnalyticsClient userAnalyticsClient,
    IUserPreferencesRepository userPreferencesRepository,
    ILogger<UserPreferencesService> logger) : IUserPreferencesService
{
    private readonly IUserAnalyticsClient _userAnalyticsClient = userAnalyticsClient;
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
            
            // here user mail should be sent
            await _userAnalyticsClient.SendUserAction(
                userId, "UserPreferencesCreated", "recipient", "User's Preferences", ct);

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

            // here user mail should be sent
            await _userAnalyticsClient.SendUserAction(
                userPreferences.UserId, "UserPreferencesUpdated", "recipient", "User's Preferences", ct);

            return UserPreferencesMapper.ToDto(entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update user preferences");
            return UserPreferencesErrors.FailedToUpdate;
        }
    }
    
    public async Task<ErrorOr<ChannelDescriptorBaseDto>> GetChannelDeliveryInfo(
        string recipientUserId, string channel, CancellationToken ct)
    {
        var byUserId = new AdHocSpecification<UserPreferences>(
            x => x.UserId == recipientUserId);
        var currentUserPreferences = await _userPreferencesRepository.FirstOrDefault(byUserId);
        if (currentUserPreferences is null)
        {
            return UserPreferencesErrors.ChannelNotFound;
        }

        var channelKey = channel.ToPascalCase();
        var channelDescriptor = currentUserPreferences.Channels.GetValueOrDefault(channelKey);
        return channelDescriptor is null
            ? UserPreferencesErrors.ChannelNotFound
            : UserPreferencesChannelMapper.ToDto(channelDescriptor);
    }
}