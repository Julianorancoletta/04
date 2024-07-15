using Delivery.Email.Worker.Application.Interfaces;
using Delivery.Email.Worker.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            model.QueueDeclare(queue: _rabbitConfiguration.Queue,
                                    durable: true,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);
            return model;
        }

        public EventingBasicConsumer Consumer()
        {
            var channel = QueueDeclare();
            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume(queue: _rabbitConfiguration.Queue, autoAck: true, consumer: consumer);
            return consumer;
        }
    }
}
