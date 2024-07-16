using Delivery.Email.Worker.Application.Interfaces;
using Delivery.Email.Worker.Application;
using System.Diagnostics.CodeAnalysis;
using Delivery.Email.Worker.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Delivery.Email.Infra.Connections;

namespace Delivery.Email.Configuration;

[ExcludeFromCodeCoverage]
public static class DependencyInjectionConfig
{
    public static IServiceCollection AddMessageBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitConfiguration>(configuration.GetSection("MessageQueueConnection"));
        services.AddSingleton<IMongoClient>(sp =>
        {
            var opcoes = sp.GetRequiredService<IOptions<BookStoreDatabaseSettings>>().Value;
            return new MongoClient(opcoes.ConnectionString);
        });
        services.AddSingleton<IMessageBus, MessageBus>();
        services.AddSingleton<IRabbitConnect, RabbitConnect>();

        return services;
    }

}

