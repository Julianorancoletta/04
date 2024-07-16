


using Delivery.Email.Configuration;
using Delivery.Email.Infra.Connections;
using Delivery.Email.Infra.Service;
using Delivery.Email.Worker;
using Serilog;

await Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddMessageBus(hostContext.Configuration)
        .AddHostedService<Worker>()
        .Configure<PessoaDatabaseSettings>(hostContext.Configuration.GetSection("EmailDatabase"))
        .AddSingleton<PessoaRepositorio>();
    }).UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration))
    .Build()
    .RunAsync();

