using Delivery.Email.Worker.Application.Interfaces;
using Delivery.Email.Worker.Application;
using System.Diagnostics.CodeAnalysis;
using Delivery.Email.Worker.Configuration;
<<<<<<< HEAD
using Delivery.Email.Core.Configuration;
=======
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Delivery.Email.Infra.Connections;
>>>>>>> 28ebd5f30ff01dffc3dcddfaaa8f27897542d4cc

namespace Delivery.Email.Configuration;

[ExcludeFromCodeCoverage]
public static class DependencyInjectionConfig
{
    public static IServiceCollection AddMessageBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitConfiguration>(configuration.GetSection("MessageQueueConnection"));
<<<<<<< HEAD
        services.Configure<EmailConfiguration>(configuration.GetSection("MessageQueueConnection"));
=======
        services.AddSingleton<IMongoClient>(sp =>
        {
            var opcoes = sp.GetRequiredService<IOptions<BookStoreDatabaseSettings>>().Value;
            return new MongoClient(opcoes.ConnectionString);
        });
>>>>>>> 28ebd5f30ff01dffc3dcddfaaa8f27897542d4cc
        services.AddSingleton<IMessageBus, MessageBus>();
        services.AddSingleton<IRabbitConnect, RabbitConnect>();
        services.AddSingleton<IEmailService, EmailService>();

        return services;
    }

}

