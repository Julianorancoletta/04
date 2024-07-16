using Delivery.Email.Core.Configuration;
using RabbitMQ.Client;

namespace Delivery.Email.Worker.Application.Interfaces
{
    public interface IRabbitConnect
    {
        public IConnection TryConnect();
        public RabbitConfiguration RabbitConfiguration();
    }
}
