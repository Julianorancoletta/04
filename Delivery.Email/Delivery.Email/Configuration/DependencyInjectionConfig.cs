using Delivery.Email.Worker.Application.Interfaces;
using Delivery.Email.Worker.Application;
using System.Diagnostics.CodeAnalysis;
using Delivery.Email.Worker.Configuration;

namespace Delivery.Email.Configuration;

[ExcludeFromCodeCoverage]
public static class DependencyInjectionConfig
{
    public static IServiceCollection AddMessageBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitConfiguration>(configuration.GetSection("MessageQueueConnection"));
        services.AddSingleton<IMessageBus, MessageBus>();
        services.AddSingleton<IRabbitConnect, RabbitConnect>();

        return services;
    }

}

