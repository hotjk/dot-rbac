﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.Configuration
{
    public static class RabbitMQ
    {
        //rabbitmqctl add_vhost event_bus_exchange
        //rabbitmqctl add_user event_user event_password
        //rabbitmqctl set_permissions -p event_bus_exchange event_user ".*" ".*" ".*"
        //rabbitmqctl set_permissions -p event_bus_exchange guest ".*" ".*" ".*"
        //rabbitmqctl list_queues -p event_bus_exchange
        //rabbitmq-plugins enable rabbitmq_management
        //RabbitMQ Management Web http://localhost:15672
        public static readonly string CQRSDemoEventBusExchange = "event_bus_exchange";
        public static readonly string CQRSDemoSagaQueue = "core_saga_action_queue";
        public static readonly string CQRSDemo = "amqp://event_user:event_password@localhost:5672/" + CQRSDemoEventBusExchange;
    }
}
