using System.Runtime.InteropServices.Marshalling;
using Infrastructure.Persistence.MassTransit;
using Infrastructure.Persistence.Mongo.Abstractions;
using Infrastructure.Persistence.Mongo.Configurations;
using Infrastructure.Persistence.Mongo.Entities.Notifications;
using Infrastructure.Persistence.Mongo.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Trumpee.MassTransit;

namespace Infrastructure.Persistence;

public static class DependencyInjection
{
    public static void AddPersistence(this IServiceCollection services, IConfiguration config)
    {
        services.AddMongoDb(config);
        services.AddMassTransit(config);
    }

    private static void AddMongoDb(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<ITemplatesRepository, TemplatesRepository>();        
        services.AddScoped<INotificationsRepository, NotificationsRepository>();        

        services.Configure<MongoDbOptions>(config.GetSection(MongoDbOptions.ConfigurationSectionName));
    }

    private static void AddMassTransit(this IServiceCollection services, IConfiguration config)
    {
        services.AddConfiguredMassTransit(config);

        services.AddScoped<IMassTransitClient<Notification>, MassTransitClient<Notification>>();
    }
}