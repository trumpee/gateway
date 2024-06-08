using Infrastructure.Persistence.Mongo.Abstractions;
using Infrastructure.Persistence.Mongo.Configurations;
using Infrastructure.Persistence.Mongo.Entities.Preferences;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.Mongo.Repositories;

public class UserPreferencesRepository(IOptions<MongoDbOptions> settings)
    : MongoDbRepositoryBase<UserPreferences>(settings), IUserPreferencesRepository;
