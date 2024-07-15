using System.Diagnostics.CodeAnalysis;
using Delivery.Cadastro.Worker.Configuration;
using Delivery.Email.Worker.Application.Interfaces;
using Delivery.Email.Worker.Application;

namespace Delivery.Email.Configuration;

[ExcludeFromCodeCoverage]
public static class DependencyInjectionConfig
{
    public static IServiceCollection AddMessageBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitConfiguration>(configuration.GetSection("MessageQueueConnection"));
        services
        .AddSingleton<IMessageBus, MessageBus>()
        .AddSingleton<IRabbitConnect, RabbitConnect>();

        return services;
    }

}

