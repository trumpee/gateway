using Infrastructure.Persistence.Mongo.Entities.Preferences;

namespace Infrastructure.Persistence.Mongo.Abstractions;

public interface IUserPreferencesRepository : IMongoRepository<UserPreferences>;