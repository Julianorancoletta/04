using Delivery.Email.Worker.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Email.Worker.Application.Interfaces
{
    public interface IRabbitConnect
    {
        public IConnection TryConnect();
        public RabbitConfiguration RabbitConfiguration();
    }
}
