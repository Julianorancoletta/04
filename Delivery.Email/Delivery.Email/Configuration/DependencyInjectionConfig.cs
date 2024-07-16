using Delivery.Email.Worker.Application.Interfaces;
using Delivery.Email.Worker.Application;
using System.Diagnostics.CodeAnalysis;
using Delivery.Email.Worker.Configuration;
using Delivery.Email.Core.Configuration;
using MongoDB.Driver;
using Delivery.Email.Infra.Connections;
using Microsoft.Extensions.Options;



namespace Delivery.Email.Configuration;

[ExcludeFromCodeCoverage]
public static class DependencyInjectionConfig
{
    public static IServiceCollection AddMessageBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitConfiguration>(configuration.GetSection("MessageQueueConnection"));
        services.Configure<EmailConfiguration>(configuration.GetSection("Email"));

        //services.AddSingleton<IMongoClient>(sp =>
        //{
        //    var opcoes = sp.GetRequiredService<IOptions<PessoaDatabaseSettings>>().Value;
        //    return new MongoClient(opcoes.ConnectionString);
        //});

        services.AddSingleton<IMessageBus, MessageBus>();
        services.AddSingleton<IRabbitConnect, RabbitConnect>();
        services.AddSingleton<IEmailService, EmailService>();

        return services;
    }

}

