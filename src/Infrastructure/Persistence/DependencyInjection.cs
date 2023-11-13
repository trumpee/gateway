using Infrastructure.Persistence.Mongo.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence;

public static class DependencyInjection
{
    public static void AddPersistence(this IServiceCollection services, IConfiguration config)
    {
        services.AddMongoDb(config);
    }

    private static void AddMongoDb(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<MongoDbOptions>(config.GetSection(MongoDbOptions.ConfigurationSectionName));
    }
}