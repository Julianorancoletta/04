using Delivery.Cadastro.Worker.Configuration;
using Delivery.Core.Modelo;
using Delivery.Email.Worker.Application.Interfaces;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Delivery.Email.Worker.Application
{
    internal class MessageBus : IMessageBus
    {

        private readonly IRabbitConnect _rabbitConnect;
        private readonly RabbitConfiguration _rabbitConfiguration;
        public MessageBus(IRabbitConnect rabbitConnect)
        {
            _rabbitConnect = rabbitConnect;
            _rabbitConfiguration = _rabbitConnect.RabbitConfiguration();
        }

        public IModel QueueDeclare()
        {
            var model = _rabbitConnect.TryConnect().CreateModel();

            return model;
        }

        public void Consumer(PessoaFisica dados)
        {
            var channel = QueueDeclare();

            var mensagem = JsonSerializer.Serialize(dados);

            var corpo = Encoding.UTF8.GetBytes(mensagem);

            channel.BasicPublish(
            exchange: "",       //como não tem nada ele está pegando de ponto a ponto que é o padrão.
            routingKey: "pessoa_queue", //se não tiver nenhuma Key criada então é assumido o nome da fila.
            basicProperties: null, //É o padrão.
            body: corpo
            );
        }
    }
}
