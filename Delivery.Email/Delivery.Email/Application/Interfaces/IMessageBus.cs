using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Delivery.Email.Worker.Application.Interfaces
{
    public interface IMessageBus
    {
        public IModel QueueDeclare();
        public EventingBasicConsumer Consumer();
    }
}
