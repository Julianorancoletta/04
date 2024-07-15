using Delivery.Email.Configuration;
using Delivery.Email.Infra.Connections;
using Delivery.Email.Infra.Service;
using Delivery.Email.Worker;
using Delivery.Email.Worker.Application;
using Delivery.Email.Worker.Application.Interfaces;
using Serilog;



await Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddMessageBus(hostContext.Configuration)
        .AddHostedService<Worker>()
        .Configure<BookStoreDatabaseSettings>(hostContext.Configuration.GetSection("EmailDatabase"))
        .AddSingleton<EmailService>();
    }).UseSerilog((hostingContext, loggerConfiguration) =>loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration))
    .Build()
    .RunAsync();

