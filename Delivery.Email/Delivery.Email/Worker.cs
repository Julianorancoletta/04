using RabbitMQ.Client.Events;
using System.Text;
using Delivery.Email.Worker.Application.Interfaces;
using System.Text.Json;
using Delivery.Email.Core.Domain;
using Delivery.Email.Infra.Service;
using Serilog;
using Delivery.Email.Worker.Application;

namespace Delivery.Email.Worker;

public class Worker : BackgroundService
{
    private readonly int _intervaloMensagemWorkerAtivo = 10;
    private readonly IMessageBus _messageBus;
    private readonly PessoaRepositorio _booksService;
    private readonly IEmailService _emailService;

    public Worker(IMessageBus messageBus, PessoaRepositorio booksService, IEmailService emailService)
    {
        _messageBus = messageBus;
        _booksService = booksService;
        _emailService = emailService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _messageBus.Consumer().Received += Consumer_Received;
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(_intervaloMensagemWorkerAtivo, stoppingToken);
        }
    }

    private async void Consumer_Received(object sender, BasicDeliverEventArgs e)
    {
        try
        {
            var resposta = JsonSerializer.Deserialize<PessoaFisica>(e.Body.ToArray());

            await _booksService.CreateAsync(resposta);
            _emailService.envio(resposta);

            Log.Information($"[Nova mensagem | {DateTime.Now:yyyy-MM-dd HH:mm:ss}] " + Encoding.UTF8.GetString(e.Body.ToArray()));
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
        }

    }
}

