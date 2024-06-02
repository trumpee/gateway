using Auth0Net.DependencyInjection;
using Infrastructure.Auth0;
using Infrastructure.Auth0.Abstractions;
using Infrastructure.Auth0.Configuration;
using Infrastructure.Persistence.MassTransit;
using Infrastructure.Persistence.MassTransit.Analytics;
using Infrastructure.Persistence.Mongo.Abstractions;
using Infrastructure.Persistence.Mongo.Configurations;
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
        services.AddAuth0(config);
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
        
        services.AddScoped<ITemplateFillerClient, TemplateFillerClient>();
        services.AddScoped<IUserAnalyticsClient, UserAnalyticsClient>();
        services.AddScoped<INotificationsAnalyticsClient, NotificationsAnalyticsClient>();
    }
    
    private static void AddAuth0(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<Auth0Options>(config.GetSection(Auth0Options.SectionName));
        
        services.AddAuth0AuthenticationClient(c =>
        {
            var domain = config["Auth0:Domain"];
            ArgumentNullException.ThrowIfNull(domain);
            
            c.Domain = domain;
            c.ClientId = config["Auth0:ClientId"];
            c.ClientSecret = config["Auth0:ClientSecret"];
        });
        
        services.AddScoped<IAuthService, AuthService>();
    }
}