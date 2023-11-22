using Core.Abstractions;
using Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static class DependencyInjection
{
    public static void AddGatewayCore(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<ITemplatesService, TemplatesService>();
        services.AddScoped<INotificationsService, NotificationService>();
    }
}