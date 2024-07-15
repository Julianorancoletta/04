﻿using Delivery.Email.Worker.Application.Interfaces;
using Delivery.Email.Worker.Configuration;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Delivery.Email.Worker.Application
{
    public class RabbitConnect : IRabbitConnect
    {
        private readonly RabbitConfiguration _rabbitConfiguration;
        public RabbitConnect(IOptions<RabbitConfiguration> rabbitConfiguration)
        {
            _rabbitConfiguration = rabbitConfiguration.Value;
        }

        public RabbitConfiguration RabbitConfiguration() => _rabbitConfiguration;



        public IConnection TryConnect()
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    Uri = new Uri(_rabbitConfiguration.ConnectionFactory)
                };

                return factory.CreateConnection();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
