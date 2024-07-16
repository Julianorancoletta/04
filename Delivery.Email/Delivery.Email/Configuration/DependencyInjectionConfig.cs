using Delivery.Email.Worker.Application.Interfaces;
using Delivery.Email.Worker.Application;
using System.Diagnostics.CodeAnalysis;
using Delivery.Email.Worker.Configuration;
using Delivery.Email.Core.Configuration;

namespace Delivery.Email.Configuration;

[ExcludeFromCodeCoverage]
public static class DependencyInjectionConfig
{
    public static IServiceCollection AddMessageBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitConfiguration>(configuration.GetSection("MessageQueueConnection"));
        services.Configure<EmailConfiguration>(configuration.GetSection("MessageQueueConnection"));
        services.AddSingleton<IMessageBus, MessageBus>();
        services.AddSingleton<IRabbitConnect, RabbitConnect>();
        services.AddSingleton<IEmailService, EmailService>();

        return services;
    }

}

