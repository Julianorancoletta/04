using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Email.Worker.Configuration
{
    public class RabbitConfiguration
    {
        public string ConnectionFactory { get; set; }
        public string Exchange { get; set; }
        public string Queue { get; set; }
        public string RoutingKey { get; set; }
        public string Type { get; set; }
        public bool Durable { get; set; }
    }
}
