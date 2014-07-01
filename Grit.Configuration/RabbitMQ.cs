using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.Configuration
{
    public static class RabbitMQ
    {
        public static readonly string CQRSDemo = "amqp://event_user:event_password@localhost:5672/grit_demo_vhost";
    }
}
