using Core.Errors;
using Core.Mappers;
using Infrastructure.Persistence.Mongo.Abstractions;
using Infrastructure.Persistence.Mongo.Entities.Preferences;
using Microsoft.Extensions.Logging;
using ErrorOr;

namespace Core.Services;

public interface IUserPreferencesService
{
    Task<ErrorOr<UserPreferencesDto>> CreateUserPreferences(string userId);
    Task<UserPreferencesDto> UpdateUserPreferences(string userId, UserPreferencesDto userPreferences);
}

public class UserPreferencesService(
    IMongoRepository<UserPreferences> userPreferencesRepository,
    ILogger<UserPreferencesService> logger) : IUserPreferencesService
{
    private readonly IMongoRepository<UserPreferences> _userPreferencesRepository = userPreferencesRepository;
    private readonly ILogger<UserPreferencesService> _logger = logger;
    
    public async Task<ErrorOr<UserPreferencesDto>> CreateUserPreferences(string userId)
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
    
    public Task<UserPreferencesDto> UpdateUserPreferences(string userId, UserPreferencesDto userPreferences)
    {
        throw new NotImplementedException();
    }
}


public record UserPreferencesDto
{
    public string? Id { get; init; }
    public required string UserId { get; init; }
    public required Dictionary<string, ChannelDescriptorBaseDto> Channels { get; init; }
}

public record ChannelDescriptorBaseDto
{
    public bool Enabled { get; init; }
    public string? Description { get; init; }
    public Dictionary<string, string>? Metadata { get; init; }
}