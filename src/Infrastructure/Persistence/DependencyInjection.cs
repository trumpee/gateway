using Infrastructure.Persistence.Mongo.Abstractions;
using Infrastructure.Persistence.Mongo.Configurations;
using Infrastructure.Persistence.Mongo.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence;

public static class DependencyInjection
{
    public static void AddPersistence(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<ITemplatesRepository, TemplatesRepository>();        
        services.AddScoped<INotificationsRepository, NotificationsRepository>();        

        services.AddMongoDb(config);
    }

    private static void AddMongoDb(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<MongoDbOptions>(config.GetSection(MongoDbOptions.ConfigurationSectionName));
    }
}